import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ConfigService, Endpoint } from '../../services/config.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-endpoints-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './endpoints-dashboard.component.html',
  styleUrls: ['./endpoints-dashboard.component.css']
})

export class EndpointsDashboardComponent {
  endpoints: Endpoint[] = [];

  constructor(
    private configService: ConfigService,
    private router: Router
  ) { }

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
    this.router.navigate(['/endpoint/new']);
  }

  onRowClick(endpoint: Endpoint): void {
    this.router.navigate(['/endpoint/edit', endpoint.endpointName.toLowerCase()], {
      queryParams: { mode: 'edit' }
    });  
  }
}