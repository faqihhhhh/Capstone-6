export enum StatusDosen {
  PNS = 0,
  TetapNonPNS = 1,
  Kontrak = 2,
  TidakDiketahui = 3,
}

export enum StatusTendik {
  PNS = 0,
  Kontrak = 1,
  TidakDiketahui = 2,
}

export enum JabatanDosenTetap {
  CalonDosen = 0,
  AsistenAhli = 1,
  Lektor = 2,
  LektorKepala = 3,
  Profesor = 4,
  TidakDiketahui = 5,
}

export enum Gender {
  Pria = 0,
  Wanita = 1,
  TidakDiketahui = 2,
}

export enum Studi {
  SMA = 0,
  D3 = 1,
  D4 = 2,
  S1 = 3,
  S2 = 4,
  S3 = 5,
  TidakDiketahui = 6,
}

export enum Nikah {
  Menikah = 0,
  Lajang = 1,
  Duda = 2,
  Janda = 3,
  TidakDiketahui = 4,
}

export enum JenisP {
  Dosen = 0,
  Tendik = 1,
  TidakDiketahui = 2,
}

export interface MutatePegawaiResponse {
  success: boolean
  data: any
  statusCode: number
  errors: any
}

export interface DosenTetap {
  dosenId?: string
  nidn: string
  jabatan_Akademik: JabatanDosenTetap
  golongan: string
  tanggal_Pensiun?: Date
  proyeksi?: string
  nomor_Serdos: string
  tmt: Date
}

export interface TendikTetap {
  tendikId?: string
  jabatan: string
  golongan: string
  tanggal_Pensiun?: Date
  proyeksi?: string
  tmt: Date
}

export interface PegawaiRequest {
  id?: string
  nip: string
  nama: string
  tanggal_Lahir: Date
  tempat_Lahir: string
  jenis_Kelamin: number
  alamat: string
  telepon_Darurat: string
  pendidikan_Terakhir: number
  gelar: string
  status_Pernikahan: number
  nomor_BPJS: string
  nomor_NPWP: string
  nomor_Paspor: string
  jenis_Pegawai: number
  jenis_Dosen?: number
  dosenTetap?: DosenTetap
  jenis_Tendik?: number
  tendikTetap?: TendikTetap
}

export interface PegawaiResponse {
  id: string
  nip: string
  nama: string
  tanggal_Lahir: Date
  tempat_Lahir: string
  jenis_Kelamin: Gender
  alamat: string
  telepon_Darurat: string
  pendidikan_Terakhir: Studi
  gelar: string
  status_Pernikahan: Nikah
  nomor_BPJS: string
  nomor_NPWP: string
  nomor_Paspor: string
  jenis_Pegawai?: JenisP
}

export interface DosenResponse extends PegawaiResponse {
  jenis_Dosen: StatusDosen
  dosenTetap?: DosenTetap | null
}

export interface TendikResponse extends PegawaiResponse {
  jenis_Tendik: StatusTendik
  tendikTetap: TendikTetap | null
}

export interface EditPegawaiRequest {
  id: string
  nip: string
  nama: string
  tanggal_Lahir: Date
  tempat_Lahir: string
  jenis_Kelamin: Gender
  alamat: string
  telepon_Darurat: string
  pendidikan_Terakhir: Studi
  gelar: string
  status_Pernikahan: Nikah
  nomor_BPJS: string
  nomor_NPWP: string
  nomor_Paspor: string
  jenis_Pegawai?: JenisP
  jenis_Dosen?: StatusDosen
  dosenTetap?: DosenTetap | null
  jenis_Tendik?: StatusTendik
  tendikTetap?: TendikTetap | null
}
