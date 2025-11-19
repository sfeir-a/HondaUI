import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigService, Endpoint } from '../../services/config.service';
import { formatFrequency } from '../../utils/time-utils';

interface FieldOption {
  name: string;
  selected: boolean;
}

@Component({
  selector: 'app-endpoint-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './endpoint-detail.component.html',
  styleUrls: ['./endpoint-detail.component.css']
})
export class EndpointDetailComponent implements OnInit {
  isEditMode = false;
  applicationName = '';
  endpointUrl = '';

  updateFrequency = 30;
  displayFrequency = 30;
  selectedUnit: 'minutes' | 'hours' | 'days' | 'months' = 'minutes';

  availableFields: FieldOption[] = [];
  errorMessage: string | null = null;
  endpointStatus: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private configService: ConfigService
  ) { }

  ngOnInit(): void {
    this.configService.getAvailableFields().subscribe({
      next: (fields: string[]) => {
        this.availableFields = fields.map(name => ({ name, selected: false }));

        const idParam = this.route.snapshot.paramMap.get('id'); 
        const mode = this.route.snapshot.queryParamMap.get('mode');
        if (mode === 'edit' && idParam) {
          this.isEditMode = true;
          this.loadEndpointData(parseInt(idParam, 10)); 
        }
      },
      error: err => console.error('Failed to load field list', err)
    });
  }

  loadEndpointData(id: number): void {
    this.configService.getById(id).subscribe({ 
      next: (endpoint: Endpoint) => {
        this.applicationName = endpoint.endpointName;
        this.endpointUrl = endpoint.url;
        this.updateFrequency = endpoint.frequency;
        this.endpointStatus = endpoint.status;

        const formatted = formatFrequency(this.updateFrequency);
        const [value, unit] = formatted.split(' ');
        this.displayFrequency = parseFloat(value);
        this.selectedUnit = unit as any;

        this.availableFields.forEach(f => {
          f.selected = endpoint.enabledFields.includes(f.name);
        });
      },
      error: err => console.error('Failed to load endpoint', err)
    });
  }

  convertToMinutes(value: number, unit: string): number {
    switch (unit) {
      case 'hours': return value * 60;
      case 'days': return value * 60 * 24;
      case 'months': return value * 60 * 24 * 30;
      default: return value;
    }
  }

  onUnitChange(): void {
    this.updateFrequency = this.convertToMinutes(this.displayFrequency, this.selectedUnit);
  }

  onGoBack(): void {
    this.router.navigate(['/']);
  }

  onDelete(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (!idParam) return;

    const id = parseInt(idParam, 10);

    if (confirm('Are you sure you want to delete this endpoint?')) {
      this.configService.delete(id).subscribe({
        next: () => this.router.navigate(['/']),
        error: err => {
          console.error('Delete failed:', err);
          this.errorMessage = 'Failed to delete endpoint.';
        }
      });
    }
  }

  onToggleStatus(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (!idParam) return;

    const id = parseInt(idParam, 10);

    if (confirm('Are you sure you want to update status of this endpoint?')) {
      this.configService.toggleStatus(id).subscribe({
        next: () => this.router.navigate(['/']),
        error: err => {
          console.error('Update failed:', err);
          this.errorMessage = 'Failed to update endpoint.';
        }
      });
    }
  }

  onSave(): void {
    if (isNaN(this.displayFrequency) || this.displayFrequency <= 0) {
      this.errorMessage = 'Please enter a valid positive number for frequency.';
      return;
    }

    const payload: any = {
      endpointName: this.applicationName,
      url: this.endpointUrl,
      frequency: Math.round(this.convertToMinutes(this.displayFrequency, this.selectedUnit))
    };

    this.availableFields.forEach(f => {
      payload[f.name] = f.selected;
    });

    const idParam = this.route.snapshot.paramMap.get('id');
    const id = idParam ? parseInt(idParam, 10) : null;

    const request$ = this.isEditMode && id
      ? this.configService.update(id, payload)
      : this.configService.create(payload);

    request$.subscribe({
      next: () => this.router.navigate(['/']),
      error: err => {
        if (err.status === 400 && err.error?.errors) {
          this.errorMessage = Object.values(err.error.errors).flat()[0] as string;
        } else {
          this.errorMessage = 'An unexpected error occurred.';
        }
      }
    });
  }

  toggleField(field: FieldOption): void {
    field.selected = !field.selected;
  }
}
