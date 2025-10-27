import { Routes } from '@angular/router';
import { EndpointsDashboardComponent } from './components/endpoints-dashboard/endpoints-dashboard.component';
import { EndpointDetailComponent } from './components/endpoint-detail/endpoint-detail.component';

export const routes: Routes = [
  { path: '', component: EndpointsDashboardComponent },
  { path: 'endpoint/new', component: EndpointDetailComponent },
  { path: 'endpoint/edit/:endpointName', component: EndpointDetailComponent }
];