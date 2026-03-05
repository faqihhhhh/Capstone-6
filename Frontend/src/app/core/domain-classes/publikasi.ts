export enum KategoriPubs {
  Scopus = 'Scopus',
  InternationalNonScopus = 'InternationalNonScopus',
  Sinta = 'Sinta',
  HKI = 'HKI',
}

export enum PenerapanMasy {
  Ya = 'Ya',
  Tidak = 'Tidak',
}

/*
Definisi Struktur atau tipe data dari API.
Untuk penamaan "Request"" menyatakan response untuk POST,PUT.
Sedangkan penamaan "Response" menyatakan response untuk Method GET
Interface ini menyerupai nilai yang diminta oleh JSON untuk method POST di endpoint
*/
export interface GetPublikasiResponse {
  publikasiId: string
  pegawaiId: string
  nip: string
  nama: string
  tahunPublikasi: number
  judulPublikasi: string
  kategoriPublikasi: string
  penerapanMasyarakat: string
}

export interface GetRingkasanPublikasiResponse {
  nip: string
  nama: string
  totalScopus: number
  totalNonScopus: number
  totalSinta: number
  totalHKI: number
  totalPenerapanMasyarakat: number
}

export interface PostPublikasiRequest {
  pegawaiId: string
  tahunPublikasi: number
  judulPublikasi: string
  kategoriPublikasi: KategoriPubs
  penerapanMasyarakat: PenerapanMasy
}

export interface EditPublikasiRequest {
  tahunPublikasi: number
  judulPublikasi: string
  kategoriPublikasi: KategoriPubs
  penerapanMasyarakat: PenerapanMasy
}

export interface MutatePublikasiResponse {
  success: boolean
  data: any
  statusCode: number
  errors: any
}
/*
export enum KategoriPubs
{
  Scopus = 0,
  InternationalNonScopus = 1,
  Sinta = 2,
  HKI = 3,
}

export enum PenerapanMasy
{
  Ya = 0,
  Tidak = 1,
  TidakDiketahui = 2,
}
*/
