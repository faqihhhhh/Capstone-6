export interface AllStatusData {
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
  lulusanTahun: number
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
