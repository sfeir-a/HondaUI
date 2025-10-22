import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

// The exact backend response.
export interface RawConfig {
  endpointName: string;
  frequency: number;
  url: string;
  [key: string]: any;
}

// The UI-ready model.
export interface Endpoint {
  endpointName: string;
  frequency: number;
  url: string;
  enabledFields: string[];
}

@Injectable({
  providedIn: 'root'
})

export class ConfigService {
  private baseUrl = 'http://localhost:5062/api/config';

  constructor(private http: HttpClient) { }

  /** 
  * Converts a RawConfig object from the backend 
  * into a frontend-friendly Endpoint model.
  */
  private transformToEndpoint(item: RawConfig): Endpoint {
    const enabledFields = Object.keys(item).filter(
      key => typeof item[key] === 'boolean' && item[key] === true
    );

    return {
      endpointName: item.endpointName,
      frequency: item.frequency,
      url: item.url,
      enabledFields
    };
  }

  // GET all configurations
  getAll(): Observable<Endpoint[]> {
    return this.http.get<RawConfig[]>(this.baseUrl).pipe(
      map((data: RawConfig[]) => data.map(this.transformToEndpoint))
    );
  }

  // GET one configuration
  getByName(name: string): Observable<Endpoint> {
    return this.http.get<RawConfig>(`${this.baseUrl}/${name}`).pipe(
      map(this.transformToEndpoint)
    );
  }
}
