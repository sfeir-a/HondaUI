import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

interface Endpoint {
  application: string;
  endpoint: string;
  fields: string;
  frequency: string;
  status: string;
}

@Component({
  selector: 'app-endpoints-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './endpoints-dashboard.component.html',
  styleUrls: ['./endpoints-dashboard.component.css']
})
export class EndpointsDashboardComponent {
  endpoints: Endpoint[] = [
    {
      application: 'Kronos',
      endpoint: '/api/kronos',
      fields: 'EMPLID; BADGEID; LAST_NAME; FIRST_NAME; CONTR_NO; ...',
      frequency: '30 min',
      status: 'Active'
    },
    {
      application: 'AEP Line',
      endpoint: '/api/aep',
      fields: 'EMPLID; BADGEID; LAST_NAME; FIRST_NAME;',
      frequency: '60 min',
      status: 'Active'
    },
    {
      application: 'ELP CAB',
      endpoint: '/api/elp_cab',
      fields: 'EMPLID; BADGEID; LAST_NAME; FIRST_NAME;',
      frequency: '30 min',
      status: 'Active'
    },
    {
      application: 'MDRS',
      endpoint: '/api/mdrs',
      fields: 'EMPLID; BADGEID;',
      frequency: '10 min',
      status: 'Active'
    }
  ];

  onAddNew(): void {
    console.log('Add new clicked');
  }

  onRowClick(endpoint: Endpoint): void {
    console.log('Endpoint clicked:', endpoint);
  }
}