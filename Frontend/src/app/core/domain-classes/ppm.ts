export enum Jenis_PPM {
  Penelitian = 'Penelitian',
  Pengabdian = 'Pengabdian',
}

export enum Mitra_PPM {
  NonMitra = 'NonMitra',
  Pemerintah = 'Pemerintah',
  Swasta = 'Swasta',
  LuarNegeri = 'LuarNegeri',
}

export interface MutatePPMResponse {
  success: boolean
  data: any
  statusCode: number
  errors: any
}

/*
Definisi Struktur atau tipe data dari API.
Untuk penamaan "Request"" menyatakan response untuk POST,PUT.
Sedangkan penamaan "Response" menyatakan response untuk Method GET
Interface ini menyerupai nilai yang diminta oleh JSON untuk method POST di endpoint
*/
export interface AddPPMRequest {
  judulPPM: string
  tahunMulai: number
  tahunSelesai: number
  jenisPPM: Jenis_PPM
  mitraPPM: Mitra_PPM
  nomorKontrak: string
  danaPPM: number
}

export interface AddRelasiRequest {
  pegawaiID: string
  kegiatanID: string
}

export interface EditPPMRequest {
  judulPPM: string
  tahunMulai: number
  tahunSelesai: number
  jenisPPM: Jenis_PPM
  mitraPPM: Mitra_PPM
  nomorKontrak: string
  danaPPM: number
}

export interface GetPPMResponse {
  kegiatanID: string
  judulPPM: string
  tahunMulai: number
  tahunSelesai: number
  jenisPPM: string
  mitraPPM: string
  //nomorKontrak: string
  //danaPPM: number
}

export interface GetPegawaiPPMResponse {
  pegawaiID: string
  namaPegawai: string
  nip: string
  jenisDosen: string
  kegiatanID: string
  judulPPM: string
  tahunMulai: number
  tahunSelesai: number
  jenisPPM: string
  mitraPPM: string
  nomorKontrak: string
  danaPPM: number
}

export interface GetRingkasanPPMResponse {
  namaPegawai: string
  nip: string
  jenisDosen: string
  jumlahPenelitian: number
  jumlahPengabdian: number
}

export interface GetMitraPenelitianResponse {
  tahun: number
  jumlahMitraPemerintah: number
  jumlahMitraSwasta: number
  jumlahMitraLuarNegeri: number
  totalDana: number
}

export interface GetMitraPengabdianResponse {
  tahun: number
  jumlahMitraPemerintah: number
  jumlahMitraSwasta: number
  jumlahMitraLuarNegeri: number
  totalDana: number
}
