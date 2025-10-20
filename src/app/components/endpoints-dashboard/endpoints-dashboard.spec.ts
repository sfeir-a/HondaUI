import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EndpointsDashboardComponent } from './endpoints-dashboard.component';

describe('EndpointsDashboard', () => {
  let component: EndpointsDashboardComponent;
  let fixture: ComponentFixture<EndpointsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EndpointsDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EndpointsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
