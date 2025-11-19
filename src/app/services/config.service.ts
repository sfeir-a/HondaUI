import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

// The exact backend response.
export interface RawConfig {
  id: number;                       // NEW FIELD
  endpointName: string;
  frequency: number;
  url: string;
  [key: string]: any;
}

// The UI-ready model.
export interface Endpoint {
  id: number;                       // NEW FIELD
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
      id: item.id,                  // ADDED
      endpointName: item.endpointName,
      frequency: item.frequency,
      url: item.url,
      enabledFields
    };
  }

  // GET all configurations
  getAll(): Observable<Endpoint[]> {
    return this.http.get<RawConfig[]>(this.baseUrl).pipe(
      map((data: RawConfig[]) => data.map(x => this.transformToEndpoint(x)))
    );
  }

  // GET one configuration
  getById(id: number): Observable<Endpoint> {
    return this.http.get<RawConfig>(`${this.baseUrl}/${id}`).pipe(
      map(x => this.transformToEndpoint(x))
    );
  }

  create(payload: any): Observable<any> {
    return this.http.post(this.baseUrl, payload);
  }

  update(id: number, payload: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  // GET available field names
  getAvailableFields(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/fields`);
  }
}
