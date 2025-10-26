// endpoint-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

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
  
  availableFields: FieldOption[] = [
    { name: 'EMPLID', selected: false },
    { name: 'BADGEID', selected: false },
    { name: 'LAST_NAME', selected: false },
    { name: 'FIRST_NAME', selected: false },
    { name: 'MIDDLE_NAME', selected: false },
    { name: 'CONTR_NO', selected: false },
    { name: 'NET_ID', selected: false },
    { name: 'COMPANY_NAME', selected: false },
    { name: 'CREATE_PROGRAM', selected: false },
    { name: 'CREATE_TSTP', selected: false },
    { name: 'ROWID', selected: false },
    { name: 'ROWSTAMP', selected: false },
    { name: 'lastchanged', selected: false },
    { name: 'BadgeType', selected: false }
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Check if we're editing an existing endpoint
    this.route.queryParams.subscribe(params => {
      if (params['mode'] === 'edit') {
        this.isEditMode = true;
        this.loadEndpointData();
      }
    });
  }

  loadEndpointData(): void {
    // For now, load mock data for Kronos endpoint
    // In real implementation, this would fetch from a service
    this.applicationName = 'Kronos';
    this.endpointUrl = '/api/kronos';
    this.updateFrequency = 30;
    
    // Pre-select certain fields for edit mode
    const selectedFieldNames = ['EMPLID', 'BADGEID', 'LAST_NAME', 'FIRST_NAME', 'MIDDLE_NAME', 'CONTR_NO', 'NET_ID'];
    this.availableFields.forEach(field => {
      if (selectedFieldNames.includes(field.name)) {
        field.selected = true;
      }
    });
  }

  onGoBack(): void {
    this.router.navigate(['/']);
  }

  onDelete(): void {
    if (confirm('Are you sure you want to delete this endpoint?')) {
      console.log('Deleting endpoint:', this.applicationName);
      this.router.navigate(['/']);
    }
  }

  onDeactivate(): void {
    console.log('Deactivating endpoint:', this.applicationName);
    this.router.navigate(['/']);
  }

  onSave(): void {
    const selectedFields = this.availableFields
      .filter(field => field.selected)
      .map(field => field.name);

    console.log('Saving endpoint:', {
      applicationName: this.applicationName,
      endpointUrl: this.endpointUrl,
      updateFrequency: this.updateFrequency,
      selectedFields: selectedFields
    });

    this.router.navigate(['/']);
  }

  toggleField(field: FieldOption): void {
    field.selected = !field.selected;
  }
}