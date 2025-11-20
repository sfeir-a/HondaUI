import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ExtractConfigurationDto {
  id: number;
  endpointName: string;
  frequency: number;
  url: string | null;
  status: boolean;

  credentialUser: string | null;
  credentialPassword: string | null;
  hasCredentialPassword: boolean;

  activeFields: string[];
}

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private baseUrl = 'http://localhost:5062/api/config';

  constructor(private http: HttpClient) { }

  // GET all configs (no mapping required)
  getAll(): Observable<ExtractConfigurationDto[]> {
    return this.http.get<ExtractConfigurationDto[]>(this.baseUrl);
  }

  // GET one config
  getById(id: number): Observable<ExtractConfigurationDto> {
    return this.http.get<ExtractConfigurationDto>(`${this.baseUrl}/${id}`);
  }

  // POST create
  create(payload: ExtractConfigurationDto): Observable<any> {
    return this.http.post(this.baseUrl, payload);
  }

  // PUT update
  update(id: number, payload: ExtractConfigurationDto): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, payload);
  }

  // DELETE delete
  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  // GET list of all available fields (for UI checkboxes)
  getAvailableFields(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/fields`);
  }
}
