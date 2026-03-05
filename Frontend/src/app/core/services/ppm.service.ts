import { Injectable } from '@angular/core'
import { HttpClient, HttpEvent, HttpParams, HttpResponse } from '@angular/common/http'
import { CommonError } from '@core/error-handler/common-error'
import { CommonHttpErrorService } from '@core/error-handler/common-http-error.service'
import { Observable, of } from 'rxjs'
import { catchError } from 'rxjs/operators'
import { map } from 'rxjs/operators'
import {
  MutatePPMResponse,
  AddPPMRequest,
  AddRelasiRequest,
  GetPPMResponse,
  GetPegawaiPPMResponse,
  GetRingkasanPPMResponse,
  GetMitraPenelitianResponse,
  GetMitraPengabdianResponse,
  EditPPMRequest,
} from '@core/domain-classes/ppm'
@Injectable({ providedIn: 'root' })
export class PpmService {
  //Endpoint untuk Service Kegiatan PPM
  private apiUrl = `api/KegiatanPPM` // Base URL untuk controller API
  constructor(
    private httpClient: HttpClient,
    private commonHttpErrorService: CommonHttpErrorService
  ) {}

  getPPMs(): Observable<GetPPMResponse[] | CommonError> {
    const url = `${this.apiUrl}/GetAll`
    return this.httpClient.get<GetPPMResponse[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getPPMPegawais(): Observable<GetPegawaiPPMResponse[] | CommonError> {
    const url = `${this.apiUrl}/GetAllRelations`
    return this.httpClient.get<GetPegawaiPPMResponse[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getRingkasanPPM(): Observable<GetRingkasanPPMResponse[] | CommonError> {
    const url = `${this.apiUrl}/RingkasanPengabdianPenelitian`
    return this.httpClient.get<GetRingkasanPPMResponse[]>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getMitraPenelitian(): Observable<GetMitraPenelitianResponse[] | CommonError> {
    const url = `${this.apiUrl}/MitraPenelitian`
    return this.httpClient
      .get<GetMitraPenelitianResponse[]>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getMitraPengabdian(): Observable<GetMitraPengabdianResponse[] | CommonError> {
    const url = `${this.apiUrl}/MitraPengabdian`
    return this.httpClient
      .get<GetMitraPengabdianResponse[]>(url)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getKegiatanByJudulAndTahun(judulPPM: string, tahunMulai: number): Observable<any> {
    const params = new HttpParams().set('judulPPM', judulPPM).set('tahunMulai', tahunMulai.toString())
    return this.httpClient.get(`${this.apiUrl}/cariPPM`, { params })
  }

  addKegiatanPPM(ppm: AddPPMRequest): Observable<MutatePPMResponse | CommonError> {
    const url = `${this.apiUrl}/AddPPM`
    return this.httpClient
      .post<MutatePPMResponse | CommonError>(url, ppm)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  addRelasiPegawaiPPM(ppm: AddRelasiRequest): Observable<MutatePPMResponse | CommonError> {
    const url = `${this.apiUrl}/AddRelation`
    return this.httpClient
      .post<MutatePPMResponse | CommonError>(url, ppm)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  editKegiatanPPM(kegiatanId: string, ppm: EditPPMRequest): Observable<MutatePPMResponse | CommonError> {
    const useApi = false // Set `false` untuk endpoint tanpa `api`
    const url = `${this.apiUrl}/update-ppm/${kegiatanId}`
    return this.httpClient
      .put<MutatePPMResponse | CommonError>(url, ppm)
      .pipe(catchError(this.commonHttpErrorService.handleError))
  }

  deleteKegiatanPPM(kegiatanId: string): Observable<MutatePPMResponse | CommonError> {
    const url = `${this.apiUrl}/delete-ppm/${kegiatanId}`
    return this.httpClient.delete<any>(url).pipe(catchError(this.commonHttpErrorService.handleError))
  }

  getAllPpm(): Observable<{ kegiatanID: string; judulPPM: string; tahunMulai: string }[]> {
    const url = `${this.apiUrl}/GetAll`
    return this.httpClient.get<any[]>(url).pipe(
      map((data) =>
        data.map((ppm) => ({
          kegiatanID: ppm.kegiatanID,
          judulPPM: ppm.JudulPPM,
          tahunMulai: ppm.tahunMulai,
        }))
      )
    )
  }
}
