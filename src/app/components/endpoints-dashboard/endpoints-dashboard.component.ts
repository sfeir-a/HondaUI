import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ConfigService, Endpoint } from '../../services/config.service';

@Component({
  selector: 'app-endpoints-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './endpoints-dashboard.component.html',
  styleUrls: ['./endpoints-dashboard.component.css']
})

export class EndpointsDashboardComponent {
  endpoints: Endpoint[] = [];

  constructor(private configService: ConfigService) { }

  ngOnInit(): void {
    this.configService.getAll().subscribe({
      next: data => {
        this.endpoints = data;
        console.log('Fetched endpoints:', data);
      },
      error: err => console.error('Error fetching data:', err)
    });
  }

  onAddNew(): void {
    console.log('Add new clicked');
  }

  onRowClick(endpoint: Endpoint): void {
    console.log('Endpoint clicked:', endpoint);
  }
}