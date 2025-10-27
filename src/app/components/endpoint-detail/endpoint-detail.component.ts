// endpoint-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfigService, Endpoint } from '../../services/config.service';

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
  isEditMode: boolean = false;
  applicationName: string = '';
  endpointUrl: string = '';
  updateFrequency: number = 30;
  availableFields: FieldOption[] = [];
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private configService: ConfigService
  ) { }

  ngOnInit(): void {
    // Load all field names dynamically
    this.configService.getAvailableFields().subscribe({
      next: (fields: string[]) => {
        // Initialize the checkbox list dynamically
        this.availableFields = fields.map(name => ({ name, selected: false }));

        // Load endpoint details if in edit mode
        const name = this.route.snapshot.paramMap.get('endpointName');
        const mode = this.route.snapshot.queryParamMap.get('mode');
        if (mode === 'edit' && name) {
          this.isEditMode = true;
          this.loadEndpointData(name);
        }
      },
      error: err => console.error('Failed to load field list', err)
    });
  }

  loadEndpointData(name: string): void {
    this.configService.getByName(name).subscribe({
      next: (endpoint: any) => {
        // Populate basic info
        this.applicationName = endpoint.endpointName;
        this.endpointUrl = endpoint.url;
        this.updateFrequency = endpoint.frequency;

        // Mark fields as selected if their corresponding property is true
        this.availableFields.forEach(f => {
          f.selected = endpoint.enabledFields.includes(f.name);
        });
      },
      error: err => console.error('Failed to load endpoint', err)
    });
  }


  onGoBack(): void {
    this.router.navigate(['/']);
  }

  onDelete(): void {
    if (!this.applicationName) return;

    if (confirm('Are you sure you want to delete this endpoint?')) {
      this.configService.delete(this.applicationName).subscribe({
        next: () => {
          console.log('Deleted endpoint:', this.applicationName);
          this.router.navigate(['/']);
        },
        error: err => {
          console.error('Delete failed:', err);
          this.errorMessage = 'Failed to delete endpoint.';
        }
      });
    }
  }


  // onDeactivate(): void {
  //   console.log('Deactivating endpoint:', this.applicationName);
  //   this.router.navigate(['/']);
  // }

  onSave(): void {
    const payload: any = {
      endpointName: this.applicationName,
      url: this.endpointUrl,
      frequency: this.updateFrequency
    };

    this.availableFields.forEach(f => {
      payload[f.name] = f.selected;
    });

    const request$ = this.isEditMode
      ? this.configService.update(this.applicationName, payload)
      : this.configService.create(payload);

    request$.subscribe({
      next: () => this.router.navigate(['/']),
      error: err => {
        if (err.status === 400 && err.error?.errors) {
          // Extract first validation message
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