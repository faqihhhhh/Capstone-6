import { Injectable } from '@angular/core'
import { HttpClient, HttpEvent, HttpParams, HttpResponse } from '@angular/common/http'
import { CommonError } from '@core/error-handler/common-error'
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service'
import { Observable, of } from 'rxjs'
import { catchError } from 'rxjs/operators'
import { map } from 'rxjs/operators'
import {
  MutatePublikasiResponse,
  PostPublikasiRequest,
  EditPublikasiRequest,
  GetPublikasiResponse,
  GetRingkasanPublikasiResponse,
} from '@core/domain-classes/publikasi'
@Injectable({ providedIn: 'root' })
export class PublikasiService {
  //Endpoint untuk Modul Luaran Publikasi
  private apiUrl = `api/LuaranPublikasi` // Base URL untuk controller API
  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService
  ) {}

  getPublikasis(): Observable<GetPublikasiResponse[] | CommonError> {
    const url = `${this.apiUrl}/get-all-publikasi`
    return this.httpClient.get<GetPublikasiResponse[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getRingkasans(): Observable<GetRingkasanPublikasiResponse[] | CommonError> {
    const url = `${this.apiUrl}/ringkasan-publikasi`
    return this.httpClient
      .get<GetRingkasanPublikasiResponse[]>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addPublikasi(publikasi: PostPublikasiRequest): Observable<MutatePublikasiResponse | CommonError> {
    const url = `${this.apiUrl}/AddPublikasi`
    return this.httpClient
      .post<MutatePublikasiResponse | CommonError>(url, publikasi)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editPublikasi(
    publikasiId: string,
    publikasi: EditPublikasiRequest
  ): Observable<MutatePublikasiResponse | CommonError> {
    const url = `${this.apiUrl}/update-publikasi/${publikasiId}`
    return this.httpClient
      .put<MutatePublikasiResponse | CommonError>(url, publikasi)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deletePublikasi(publikasiId: string): Observable<MutatePublikasiResponse | CommonError> {
    const url = `${this.apiUrl}/delete-publikasi/${publikasiId}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }
}
