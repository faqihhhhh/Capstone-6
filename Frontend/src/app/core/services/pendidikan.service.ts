import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { CommonError } from '@core/error-handler/common-error'
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service'
import { Observable, of } from 'rxjs'
import { catchError } from 'rxjs/operators'
import {
  MutatePendidikanResponse,
  PendidikanResponse,
  MutatePendidikanRequest,
  MutatePendidikanEditRequest,
  MutatePendidikanDeleteRequest,
} from '@core/domain-classes/pendidikan'

@Injectable({ providedIn: 'root' })
export class KependidikanService {
  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService
  ) {}

  getKependidikans(): Observable<PendidikanResponse[] | CommonError> {
    const url = `Kependidikan/all`
    return this.httpClient.get<PendidikanResponse[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addKependidikan(kependidikan: MutatePendidikanRequest): Observable<MutatePendidikanResponse | CommonError> {
    const url = 'Kependidikan/Akademik'
    return this.httpClient
      .post<MutatePendidikanResponse | CommonError>(url, kependidikan)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editKependidikan(kependidikan: MutatePendidikanEditRequest): Observable<MutatePendidikanResponse | CommonError> {
    const url = `Kependidikan/update/by-year`
    return this.httpClient
      .put<MutatePendidikanResponse | CommonError>(url, kependidikan)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deleteKependidikan(thnMasukId: number): Observable<MutatePendidikanResponse | CommonError> {
    const url = `Kependidikan/TahunMasuk/${thnMasukId}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }
}
