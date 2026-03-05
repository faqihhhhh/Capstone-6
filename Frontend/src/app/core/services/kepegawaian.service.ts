import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { CommonError } from '@core/error-handler/common-error'
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service'
import { Observable } from 'rxjs'
import { catchError, map } from 'rxjs/operators'
import { MutatePegawaiResponse, DosenResponse, PegawaiRequest, TendikResponse } from '@core/domain-classes/kepegawaian'
@Injectable({ providedIn: 'root' })
export class KepegawaianService {
  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService
  ) {}

  getDosens(): Observable<DosenResponse[] | CommonError> {
    const url = `Pegawai/dosen`
    return this.httpClient.get<any[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getTendiks(): Observable<TendikResponse[] | CommonError> {
    const url = `Pegawai/tendik`
    return this.httpClient.get<any[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addPegawai(pegawai: PegawaiRequest): Observable<MutatePegawaiResponse | CommonError> {
    const url = 'Pegawai'
    return this.httpClient
      .post<MutatePegawaiResponse | CommonError>(url, pegawai)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editPegawai(id: string, pegawai: PegawaiRequest): Observable<MutatePegawaiResponse | CommonError> {
    const url = `Pegawai/${id}`
    return this.httpClient
      .put<MutatePegawaiResponse | CommonError>(url, pegawai)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deletePegawai(id: string): Observable<MutatePegawaiResponse | CommonError> {
    const url = `Pegawai/by-id/${id}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getAllDosen(): Observable<{ id: string; nama: string; nip: string }[]> {
    const url = `Pegawai/dosen`
    return this.httpClient.get<any[]>(url).pipe(
      map((data) =>
        data.map((dosen) => ({
          id: dosen.id,
          nama: dosen.nama,
          nip: dosen.nip,
        }))
      )
    )
  }
}
