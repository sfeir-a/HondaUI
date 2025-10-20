import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { EndpointsDashboardComponent } from './components/endpoints-dashboard/endpoints-dashboard.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EndpointsDashboardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'honda-api-ui';
}