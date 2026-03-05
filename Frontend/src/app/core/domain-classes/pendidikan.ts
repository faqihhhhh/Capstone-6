export interface MutatePendidikanResponse {
  message: string
  data: null
}

export interface MutatePendidikanRequest {
  thnAkademik: number
  semester: number
  thnMasuk: number
  totalRegistrasiMhs: number
  jmlhLulus: number
  jmlhNonAktif: number
  jmlhDO: number
  jmlhPengunduranDiri: number
  rataanIPKTotal: number
  masaStudiDibawah8: number
  masaStudi8Sampai10: number
  masaStudiDiatas10: number
  jumlahIPKDibawah2: number
  jumlhMBKM: number
}

export interface MutatePendidikanEditRequest {
  thnAkademik: number
  semester: number
  thnMasuk: number
  totalRegistrasiMhs: number
  jmlhLulus: number
  jmlhNonAktif: number
  jmlhDO: number
  jmlhUndurDiri: number
  rataanIPKTotal: number
  jumlahIPKDibawah2: number
  masaStudiDibawah8: number
  masaStudi8Sampai10: number
  masaStudiDiatas10: number
}

export interface MutatePendidikanDeleteRequest {
  thnAkademik: number
  semester: number
  thnMasuk: number
}

export interface InfoMahasiswa {
  jmlhAktif: number
  jmlhLulus: number
  jmlhNonAktif: number
  jmlhDO: number
  jmlhUndurDiri: number
  rataanIPKTotal: number
  jumlahIPKDibawah2: number
  masaStudiDibawah8: number
  masaStudi8Sampai10: number
  masaStudiDiatas10: number
}

export interface PendidikanResponse {
  thnAkademik: number
  semester: string
  deskripsi: string
  thnMasukId: number
  thnMasuk: number
  totalRegistrasiMhs: number
  infoMahsiswa: InfoMahasiswa
}
