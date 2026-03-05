export interface KemahasiswaanTracerData {
  lulusanTahun: number
  statusLulusan: StatusLulusan[]
}

export interface StatusLulusan {
  lulusanTahun?: number
  statusId: number
  tahun_Input: number
  presentase_Bekerja: number
  presentase_LanjutStudi: number
  presentase_Internship: number
  presentase_Wirausaha: number
  presentase_BelumKerja: number
  masaTungguKerja: MasaTungguKerja
  posisiJabatan: PosisiJabatan
  jenisTempatKerja: JenisTempatKerja
  tingkatTempatKerja: TingkatTempatKerja
}

export interface JenisTempatKerja {
  presentase_BUMN: number
  presentase_Organisasi_Multilateral: number
  presentase_Instansi_Pemerintah: number
  presentase_Organisasi_NonProfit: number
  presentase_Wirausaha: number
  presentase_Lainnya: number
}

export interface MasaTungguKerja {
  presentase_diatas6Bulan: number
  presentase_dibawah6Bulan: number
}

export interface PosisiJabatan {
  presentase_Founder: number
  presentase_CoFounder: number
  presentase_Staf: number
  presentase_Freelancer: number
}

export interface TingkatTempatKerja {
  presentase_Lokal: number
  presentase_Nasional: number
  presentase_MultiNasional: number
}

export interface MutateStatusLulusan {
  tahun_Input?: number
  bekerja: number
  lanjutStudi: number
  internship: number
  berwirausaha: number
  belumKerja: number
  masaTungguKerja: MutateMasaTungguKerja
  jenisTempatKerja: MutateJenisTempatKerja
  posisiJabatan: MutatePosisiJabatan
  tingkatTempatKerja: MutateTingkatTempatKerja
}

export interface MutateJenisTempatKerja {
  bumn: number
  organisasi_Multilateral: number
  instansi_Pemerintah: number
  organisasi_NonProfit: number
  wirausaha: number
  lainnya: number
}

export interface MutateMasaTungguKerja {
  diatas6Bulan: number
  dibawah6Bulan: number
}

export interface MutatePosisiJabatan {
  founder: number
  coFounder: number
  staf: number
  freelancer: number
}

export interface MutateTingkatTempatKerja {
  lokal: number
  nasional: number
  multiNasional: number
}

export interface PrestasiMhs {
  jmlhPKM: number
  jumlhMapres: number
  jumlhLombaNasional: number
  jumlhLombaInter: number
  jumlhInbound: number
  jumlhOutbound: number
}

export interface GetPrestasiMhs extends PrestasiMhs {
  thnAkademikId: number
}
export interface KemahasiswaanPrestasiData {
  thnAkademikId: number
  thnAkademik: number
  semester: string
  getPrestasiMhs: GetPrestasiMhs
}

export interface KemahasiswaanPrestasiRequest {
  thnAkademik: number
  semester: number
  prestasiMhs: PrestasiMhs
}

export interface KemahasiswaanTracerRequest {
  lulusanTahun: number
  statusLulusan: MutateStatusLulusan[]
}

export interface MutateKemahasiswaanTracerResponse {
  tahunId: number
}

export interface MutateKemahasiswaanPrestasiResponse {
  message: string
  data: null
}
