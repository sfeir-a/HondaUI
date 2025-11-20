import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ConfigService, ExtractConfigurationDto } from '../../services/config.service';
import { formatFrequency } from '../../utils/time-utils';

@Component({
  selector: 'app-endpoints-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './endpoints-dashboard.component.html',
  styleUrls: ['./endpoints-dashboard.component.css']
})
export class EndpointsDashboardComponent {

  endpoints: (ExtractConfigurationDto & { formattedFrequency: string })[] = [];

  constructor(
    private configService: ConfigService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.configService.getAll().subscribe({
      next: (data: ExtractConfigurationDto[]) => {
        this.endpoints = data.map(endpoint => ({
          ...endpoint,
          formattedFrequency: formatFrequency(endpoint.frequency)
        }));

        console.log('Fetched endpoints:', this.endpoints);
      },
      error: err => console.error('Error fetching data:', err)
    });
  }

  onAddNew(): void {
    this.router.navigate(['/endpoint/new']);
  }

  onRowClick(endpoint: ExtractConfigurationDto): void {
    this.router.navigate(['/endpoint/edit', endpoint.id], {
      queryParams: { mode: 'edit' }
    });
  }
}
