import { Injectable } from '@angular/core'
import { HttpClient, HttpEvent, HttpParams, HttpResponse } from '@angular/common/http'
import { CommonError } from '@core/error-handler/common-error'
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service'
import { Observable, of } from 'rxjs'
import { catchError } from 'rxjs/operators'
import {
  KemahasiswaanTracerRequest,
  KemahasiswaanTracerData,
  KemahasiswaanPrestasiRequest,
  KemahasiswaanPrestasiData,
  MutateKemahasiswaanPrestasiResponse,
  MutateKemahasiswaanTracerResponse,
  PrestasiMhs,
  MutateStatusLulusan,
} from '@core/domain-classes/kemahasiswaan'

@Injectable({ providedIn: 'root' })
export class KemahasiswaanService {
  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService
  ) {}

  getTracers(): Observable<KemahasiswaanTracerData[] | CommonError> {
    const url = `Kemahasiswaan/tracer/all`
    return this.httpClient.get<KemahasiswaanTracerData[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addTracer(tracer: KemahasiswaanTracerRequest): Observable<MutateKemahasiswaanTracerResponse | CommonError> {
    const url = 'Kemahasiswaan/tracer/lulusan'
    return this.httpClient
      .post<MutateKemahasiswaanTracerResponse | CommonError>(url, tracer)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editTracer(id: string, tracer: MutateStatusLulusan): Observable<MutateKemahasiswaanTracerResponse | CommonError> {
    const url = `Kemahasiswaan/tracer/${id}`
    return this.httpClient
      .put<MutateKemahasiswaanTracerResponse | CommonError>(url, tracer)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deleteTracer(id: string): Observable<MutateKemahasiswaanTracerResponse | CommonError> {
    const url = `Kemahasiswaan/tracer/${id}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getPrestasis(): Observable<KemahasiswaanPrestasiData[] | CommonError> {
    const url = `Kemahasiswaan/prestasi/all`
    return this.httpClient
      .get<KemahasiswaanPrestasiData[]>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addPrestasi(prestasi: KemahasiswaanPrestasiRequest): Observable<MutateKemahasiswaanPrestasiResponse | CommonError> {
    const url = 'Kemahasiswaan/prestasi/mahasiswa'
    return this.httpClient
      .post<MutateKemahasiswaanPrestasiResponse | CommonError>(url, prestasi)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editPrestasi(id: string, prestasi: PrestasiMhs): Observable<MutateKemahasiswaanPrestasiResponse | CommonError> {
    const url = `Kemahasiswaan/prestasi/${id}`
    return this.httpClient
      .put<MutateKemahasiswaanPrestasiResponse | CommonError>(url, prestasi)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deletePrestasi(id: string): Observable<MutateKemahasiswaanPrestasiResponse | CommonError> {
    const url = `AkademikTahun/${id}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }
}
