import { Component, OnInit, ViewChild } from '@angular/core'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { BaseComponent } from 'src/app/base.component'
import { ToastrService } from 'ngx-toastr'
import { PpmService } from '@core/services/ppm.service'
import { KepegawaianService } from '@core/services/kepegawaian.service'
import { CommonError } from '@core/error-handler/common-error'
import { FormGroup, NgForm } from '@angular/forms'
import {
  Jenis_PPM,
  Mitra_PPM,
  MutatePPMResponse,
  AddPPMRequest,
  AddRelasiRequest,
  EditPPMRequest,
  GetPPMResponse,
  GetPegawaiPPMResponse,
  GetRingkasanPPMResponse,
  GetMitraPenelitianResponse,
  GetMitraPengabdianResponse,
} from '@core/domain-classes/ppm'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service'
import { TranslationService } from '@core/services/translation.service'

//-----------------------------------------------------------------
@Component({
  selector: 'ppm',
  templateUrl: './ppm.component.html',
  styleUrls: ['./ppm.component.scss'],
})
export class PpmComponent extends BaseComponent implements OnInit {
  /*
  Variabel-variabel ini akan digunakan untuk menyimpan data yang daimabil/hit dari API Backend
  ada 5: kegiatanppms, pegawaippms, ringkasans, mitrapenelitis, mitrapengabdis
  */
  kegiatanppms: GetPPMResponse[] = []
  pegawaippms: GetPegawaiPPMResponse[] = []
  ringkasans: GetRingkasanPPMResponse[] = []
  mitrapenelitis: GetMitraPenelitianResponse[] = []
  mitrapengabdis: GetMitraPengabdianResponse[] = []
  // Variabel ini yang digunakan untuk menampilkan data ke tabel frontend tabel
  // jumlah variabel sama dengan struktur data di Backend

  dataSourceKegiatanPPM = new MatTableDataSource<GetPPMResponse>()
  dataSourcePegawaiPPM = new MatTableDataSource<GetPegawaiPPMResponse>()
  dataSourceRingkasanPPM = new MatTableDataSource<GetRingkasanPPMResponse>()
  dataSourceMitraPenelitian = new MatTableDataSource<GetMitraPengabdianResponse>()
  dataSourceMitraPengabdian = new MatTableDataSource<GetMitraPengabdianResponse>()
  //---------------------------------------------------------------------------------------------------//
  isLoadingResults = true
  //Mengambil data Dosen saat hit API PegawaiPPM
  dosenSummary: { id: string; nama: string; nip: string }[] = []
  ppmSummary: { kegiatanID: string; judulPPM: string; tahunMulai: string }[] = []
  selectedkegiatanPPMId: string | null = null
  selectedDosenId: string | null = null
  editIdTarget: string
  relasiIdTarget: string
  //simpleChartData: { name: string; value: number }[] = [];

  formAdd: FormGroup
  formEdit: FormGroup

  // Menambahkan Header/atribut yang ada di tabel
  displayedColumnsKegiatanPpm: string[] = [
    'judulPPM',
    'tahunMulai',
    'tahunSelesai',
    'jenisPPM',
    'mitraPPM',
    'assignDosen',
    'ubah',
    'hapus',
  ]

  displayedColumnsPegawaiPpm: string[] = [
    'namaPegawai',
    'nip',
    'jenisDosen',
    'judulPPM',
    'tahunMulai',
    'tahunSelesai',
    'jenisPPM',
    'mitraPPM',
    'nomorKontrak',
    'danaPPM',
  ]

  displayedColumnsRingkasanPPM: string[] = ['namaPegawai', 'nip', 'jenisDosen', 'jumlahPenelitian', 'jumlahPengabdian']
  displayedColumnsMitraPenelitian: string[] = [
    'Tahun',
    'JumlahMitraPemerintah',
    'JumlahMitraSwasta',
    'JumlahMitraLuarNegeri',
    'TotalDana',
  ]

  displayedColumnsMitraPengabdian: string[] = [
    'Tahun',
    'JumlahMitraPemerintah',
    'JumlahMitraSwasta',
    'JumlahMitraLuarNegeri',
    'TotalDana',
  ]

  // Untuk charts
  simpleChartData: { name: string; value: number }[] = []
  simpleChartData2: { name: string; value: number }[] = []
  //penelitianChartData: any[] = [];
  colorScheme = {
    domain: ['#5AA454', '#C7B42C', '#AAAAAA', '#FF5733'],
  }

  constructor(
    private commonDialogService: CommonDialogService,
    private dosenService: KepegawaianService,
    private ppmService: PpmService,
    private translationService: TranslationService,
    private toastrService: ToastrService
  ) {
    super()
  }
  // Sort table dan paginator
  // --> digunakan saat nanti HIT API kemudian data akan diurutkan dengan perintah: this.sortKegiatanPpm
  @ViewChild(MatSort) sortKegiatanPpm: MatSort
  @ViewChild(MatSort) sortPegawaiPpm: MatSort
  @ViewChild(MatSort) sortRingkasanPpm: MatSort
  @ViewChild(MatSort) sortMitraPenelitian: MatSort
  @ViewChild(MatSort) sortMitraPengabdian: MatSort

  @ViewChild('paginatorKegiatanPpm', { static: true }) paginatorKegiatanPpm: MatPaginator
  @ViewChild('paginatorPegawaiPpm', { static: true }) paginatorPegawaiPpm: MatPaginator
  @ViewChild('paginatorRingkasanPpm', { static: true }) paginatorRingkasanPpm: MatPaginator
  @ViewChild('paginatorMitraPenelitian', { static: true }) paginatorMitraPenelitian: MatPaginator
  @ViewChild('paginatorMitraPengabdian') paginatorMitraPengabdian: MatPaginator

  // Setting property
  displayAdd = 'none'
  displayEdit = 'none'
  displayRelasi = 'none'
  pageSizeOptionsKegiatanPpm: number[] = [5, 10, 20]
  pageIndexKegiatanPpm = 0
  pageSizeKegiatanPpm = 5
  pageSizeOptionsPegawaiPpm: number[] = [5, 10, 20]
  pageIndexPegawaiPpm = 0
  pageSizePegawaiPpm = 5
  pageSizeOptionsRingkasanPpm: number[] = [5, 10, 20]
  pageIndexRingkasanPpm = 0
  pageSizeRingkasanPpm = 5
  pageSizeOptionsMitraPenelitian: number[] = [5, 10, 20]
  pageIndexMitraPenelitian = 0
  pageSizeMitraPenelitian = 5
  pageSizeOptionsMitraPengabdian: number[] = [5, 10, 20]
  pageIndexMitraPengabdian = 0
  pageSizeMitraPengabdian = 5

  // Init untuk GET Tabel dan juga info Pegawai/Dosen
  ngOnInit(): void {
    this.loadDosenSummary()
    this.loadKegiatanPPMSummary()
    this.getPPMs()
    this.getPPMPegawais()
    this.getRingkasanPPM()
    this.getMitraPenelitian()
    this.getMitraPengabdian()
    this.penelitianChartData()
    this.pengabdianChartData()
    //this.processPenelitianChartData();
  }
  onPageChangeKegiatanPpm(event: any) {
    this.pageIndexKegiatanPpm = event.pageIndex
    this.pageSizeKegiatanPpm = event.pageSize
  }
  onPageChangePegawaiPpm(event: any) {
    this.pageIndexPegawaiPpm = event.pageIndex
    this.pageSizePegawaiPpm = event.pageSize
  }
  onPageChangeRingkasanPpm(event: any) {
    this.pageIndexRingkasanPpm = event.pageIndex
    this.pageSizeRingkasanPpm = event.pageSize
  }
  onPageChangeMitraPenelitian(event: any) {
    this.pageIndexMitraPenelitian = event.pageIndex
    this.pageSizeMitraPenelitian = event.pageSize
  }
  onPageChangeMitraPengabdian(event: any) {
    this.pageIndexMitraPengabdian = event.pageIndex
    this.pageSizeMitraPengabdian = event.pageSize
  }
  //-----------------------------------------------------------------

  //Pengaturan OpenModal (FORM)
  openModalAdd() {
    this.displayAdd = 'block'
  }
  onCloseModalAdd() {
    this.displayAdd = 'none'
  }
  openModalEdit(id: string) {
    console.log('Editing ID:', id)
    this.editIdTarget = id
    this.displayEdit = 'block'
  }
  onCloseModalEdit() {
    this.displayEdit = 'none'
  }

  //sepertinya harus menambahkan id untuk KegiatanId
  openModalRelasi(id: string) {
    this.relasiIdTarget = id
    this.displayRelasi = 'block'
  }
  onCloseModalRelasi() {
    this.displayRelasi = 'none'
  }

  /*
  Untuk FORM ada 3:
  onSubmitAddPpm() --> untuk menambahkan data --> addKegiatanPpm(ppm)
  onSubmitEditPpm() --> untuk mengedit --> editKegiatanPpm(IdTarget, ppm)
  onSubmitRelasiPpm() --> belum ada.. untuk POST Relasi --> addRelasiPpm(IdTarget, ppm)
  */

  onSubmitAddPpm(form: NgForm) {
    console.log(form)
    if (form.valid) {
      const ppm: AddPPMRequest = {
        judulPPM: form.value.judulPPM,
        tahunMulai: form.value.tahunMulai,
        tahunSelesai: form.value.tahunSelesai,
        jenisPPM: form.value.jenisPPM,
        mitraPPM: form.value.mitraPPM,
        nomorKontrak: form.value.nomorKontrak,
        danaPPM: form.value.danaPPM,
      }
      this.addKegiatanPPM(ppm)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEditPpm(formEdit: NgForm) {
    if (!this.editIdTarget) {
      this.toastrService.error('Kegiatan PPM tidak ditemukan!', 'Error!', { timeOut: 2500 })
      return
    }
    console.log(formEdit)
    if (formEdit.valid) {
      const ppm: EditPPMRequest = {
        judulPPM: formEdit.value.judulPPM_edit,
        tahunMulai: formEdit.value.tahunMulai_edit,
        tahunSelesai: formEdit.value.tahunSelesai_edit,
        jenisPPM: formEdit.value.jenisPPM_edit,
        mitraPPM: formEdit.value.mitraPPM_edit,
        nomorKontrak: formEdit.value.nomorKontrak_edit,
        danaPPM: formEdit.value.danaPPM_edit,
      }

      // Proceed with submission (e.g., call a service to send the data)
      console.log('Submitted Data Publikasi:', ppm)

      // Example call to a service
      this.editKegiatanPPM(this.editIdTarget, ppm)
    }
  }

  onSubmitRelasiPpm(formRelasi: NgForm) {
    if (!this.relasiIdTarget) {
      this.toastrService.error('Kegiatan PPM tidak ditemukan!', 'Error!', { timeOut: 2500 })
      return
    }
    console.log(formRelasi)
    if (formRelasi.valid) {
      const ppmRelasi: AddRelasiRequest = {
        pegawaiID: formRelasi.value.dosen,
        kegiatanID: this.relasiIdTarget,
      }
      this.addRelasiPegawaiPPM(ppmRelasi)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  getJenisPpmFromInt(value: number): string {
    return Jenis_PPM[value] || 'Tidak Diketahui'
  }

  getMitraPpmFromInt(value: number): string {
    return Mitra_PPM[value] || 'Tidak Diketahui'
  }

  /*
Chart 2 untuk Penelitian dan Pengabdian:
1. PenelitianChartData()
2. PengabdianChartData()
*/
  penelitianChartData(): void {
    const currentYear = new Date().getFullYear()

    // Definisikan kategori untuk setiap kombinasi
    const categories = [
      { jenisPPM: 'Penelitian', mitraPPM: null, name: 'Total Penelitian' },
      { jenisPPM: 'Penelitian', mitraPPM: 'NonMitra', name: 'Mandiri' },
      { jenisPPM: 'Penelitian', mitraPPM: 'Pemerintah', name: 'Mitra Pemerintah' },
      { jenisPPM: 'Penelitian', mitraPPM: 'Swasta', name: 'Mitra Swasta' },
      { jenisPPM: 'Penelitian', mitraPPM: 'LuarNegeri', name: 'Mitra Luar Negeri' },
    ]

    // Inisialisasi total jumlah untuk setiap kategori
    const totals = categories.map((category) => ({ name: category.name, value: 0 }))

    // Hitung jumlah untuk setiap kategori dalam 5 tahun terakhir
    this.kegiatanppms.forEach((ppms) => {
      const year = ppms.tahunMulai

      if (year >= currentYear - 5 && year <= currentYear) {
        categories.forEach((category) => {
          const isJenisMatch = ppms.jenisPPM === category.jenisPPM
          const isMitraMatch = category.mitraPPM === null || ppms.mitraPPM === category.mitraPPM

          if (isJenisMatch && isMitraMatch) {
            const categoryData = totals.find((item) => item.name === category.name)
            if (categoryData) {
              categoryData.value += 1 // Tambahkan jumlah untuk kategori tersebut
            }
          }
        })
      }
    })

    // Tetapkan data ke properti simpleChartData
    this.simpleChartData = totals

    console.log('Data untuk grafik:', this.simpleChartData) // Debugging
  }

  pengabdianChartData(): void {
    const currentYear = new Date().getFullYear()
    // Definisikan kategori untuk setiap kombinasi
    const categories = [
      { jenisPPM: 'Pengabdian', mitraPPM: null, name: 'Total Pengabdian' },
      { jenisPPM: 'Pengabdian', mitraPPM: 'NonMitra', name: 'Mandiri' },
      { jenisPPM: 'Pengabdian', mitraPPM: 'Pemerintah', name: 'Mitra Pemerintah' },
      { jenisPPM: 'Pengabdian', mitraPPM: 'Swasta', name: 'Mitra Swasta' },
      { jenisPPM: 'Pengabdian', mitraPPM: 'LuarNegeri', name: 'Mitra Luar Negeri' },
    ]

    // Inisialisasi total jumlah untuk setiap kategori
    const totals = categories.map((category) => ({ name: category.name, value: 0 }))

    // Hitung jumlah untuk setiap kategori dalam 5 tahun terakhir
    this.kegiatanppms.forEach((ppms) => {
      const year = ppms.tahunMulai

      if (year >= currentYear - 5 && year <= currentYear) {
        categories.forEach((category) => {
          const isJenisMatch = ppms.jenisPPM === category.jenisPPM
          const isMitraMatch = category.mitraPPM === null || ppms.mitraPPM === category.mitraPPM

          if (isJenisMatch && isMitraMatch) {
            const categoryData = totals.find((item) => item.name === category.name)
            if (categoryData) {
              categoryData.value += 1 // Tambahkan jumlah untuk kategori tersebut
            }
          }
        })
      }
    })

    // Tetapkan data ke properti simpleChartData
    this.simpleChartData2 = totals

    console.log('Data untuk grafik:', this.simpleChartData) // Debugging
  }
  //-----------------------------------

  /*
Form Export ada 4:
1. exportToCSVPpm()
2. exportToCSVRingkasanPpm()
3. exportToCSVMitraPenelitian()
4. exportToCSVMitraPengabdian()
*/

  exportToCSVPpm(): void {
    const header = [
      'namaPegawai',
      'nip',
      'jenisDosen',
      'judulPPM',
      'tahunMulai',
      'tahunSelesai',
      'jenisPPM',
      'mitraPPM',
      'nomorKontrak',
      'danaPPM',
    ]

    // Ambil data dari dataSourcePegawaiPPM
    const rows = this.dataSourcePegawaiPPM.data.map((row) => [
      row.namaPegawai,
      row.nip,
      row.jenisDosen,
      row.judulPPM,
      row.tahunMulai,
      row.tahunSelesai,
      row.jenisPPM,
      row.mitraPPM,
      row.nomorKontrak,
      row.danaPPM,
    ])

    // Gabungkan header dan data
    const csvContent = [header, ...rows]
      .map((e) => e.join(';')) // Gabungkan tiap elemen array dengan koma
      .join('\n') // Gabungkan tiap baris dengan newline

    // Buat file blob untuk diunduh
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' })
    const url = URL.createObjectURL(blob)

    // Buat link untuk unduh
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', 'Daftar Kegiatan PPM Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVRingkasanPpm(): void {
    const header = ['namaPegawai', 'nip', 'jenisDosen', 'jumlahPenelitian', 'jumlahPengabdian']
    // Ambil data dari dataSourceRingkasanPpm
    const rows = this.dataSourceRingkasanPPM.data.map((row) => [
      row.namaPegawai,
      row.nip,
      row.jenisDosen,
      row.jumlahPenelitian,
      row.jumlahPengabdian,
    ])
    // Gabungkan header dan data
    const csvContent = [header, ...rows]
      .map((e) => e.join(';')) // Gabungkan tiap elemen array dengan titik koma
      .join('\n') // Gabungkan tiap baris dengan newline

    // Buat file blob untuk diunduh
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' })
    const url = URL.createObjectURL(blob)

    // Buat link untuk unduh
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', 'Ringkasan Kegiatan Departemen ESL(5 Tahun Terakhir).csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVMitraPenelitian(): void {
    const header = ['Tahun', 'JumlahMitraPemerintah', 'JumlahMitraSwasta', 'JumlahMitraLuarNegeri', 'TotalDana']
    // Ambil data dari dataSourceRingkasanPpm
    const rows = this.dataSourceMitraPenelitian.data.map((row) => [
      row.tahun,
      row.jumlahMitraPemerintah,
      row.jumlahMitraSwasta,
      row.jumlahMitraLuarNegeri,
      row.totalDana,
    ])
    // Gabungkan header dan data
    const csvContent = [header, ...rows]
      .map((e) => e.join(';')) // Gabungkan tiap elemen array dengan titik koma
      .join('\n') // Gabungkan tiap baris dengan newline

    // Buat file blob untuk diunduh
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' })
    const url = URL.createObjectURL(blob)

    // Buat link untuk unduh
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', 'Mitra Penelitian Departemen ESL(5 Tahun Terakhir).csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVMitraPengabdian(): void {
    const header = ['Tahun', 'JumlahMitraPemerintah', 'JumlahMitraSwasta', 'JumlahMitraLuarNegeri', 'TotalDana']
    // Ambil data dari dataSourceRingkasanPpm
    const rows = this.dataSourceMitraPengabdian.data.map((row) => [
      row.tahun,
      row.jumlahMitraPemerintah,
      row.jumlahMitraSwasta,
      row.jumlahMitraLuarNegeri,
      row.totalDana,
    ])
    // Gabungkan header dan data
    const csvContent = [header, ...rows]
      .map((e) => e.join(';')) // Gabungkan tiap elemen array dengan titik koma
      .join('\n') // Gabungkan tiap baris dengan newline

    // Buat file blob untuk diunduh
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' })
    const url = URL.createObjectURL(blob)

    // Buat link untuk unduh
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', 'Mitra Pengabdian Departemen ESL(5 Tahun Terakhir).csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  addSpaceBeforeUppercase(str: string): string {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2')
  }
  //---------------------------------------------------------------------

  /*
  API CALL melalui publikasi.service
  ada 5 GET endpoint, 2 POST, 1 PUT, dan 1 DELETE Endpoint:

  # Method GET
  1.getPPMs() --> GetPPMResponse
  2.getPPMPegawais() --> GetPegawaiPPMResponse
  3.getRingkasanPPM() --> GetRingkasanPPMResponse
  4.getMitraPenelitian() --> GetMitraPenelitianResponse
  5.getMitraPengabdian() --> GetMitraPengabdianResponse
  
  #Method POST
  1. addKegiatanPPM(ppm: AddPPMRequest)
  2. addRelasiPegawaiPPM(ppm: AddRelasiRequest)

  # Method PUT
  1. editKegiatanPPM(kegiatanId: string, ppm: EditPPMRequest)

  # Method DELETE
  1. deleteKegiatanPPM(kegiatanId: string)

  */
  // # Method GET
  getPPMs(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.getPPMs().subscribe(
      (data: GetPPMResponse[]) => {
        this.isLoadingResults = false
        this.kegiatanppms = data
        this.dataSourceKegiatanPPM.data = data
        this.dataSourceKegiatanPPM.paginator = this.paginatorKegiatanPpm
        this.dataSourceKegiatanPPM.sort = this.sortKegiatanPpm
        //tambahkan class chart di sini
        this.penelitianChartData()
        this.pengabdianChartData()
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  getPPMPegawais(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.getPPMPegawais().subscribe(
      (data: GetPegawaiPPMResponse[]) => {
        this.isLoadingResults = false
        this.pegawaippms = data
        this.dataSourcePegawaiPPM.data = this.pegawaippms
        this.dataSourcePegawaiPPM.paginator = this.paginatorPegawaiPpm
        this.dataSourcePegawaiPPM.sort = this.sortPegawaiPpm
        //this.processSimpleChartData();
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  getRingkasanPPM(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.getRingkasanPPM().subscribe(
      (data: GetRingkasanPPMResponse[]) => {
        this.isLoadingResults = false
        this.ringkasans = data
        //this.proyeksiTendik = this.transformTendiksData(data)
        this.dataSourceRingkasanPPM.data = data
        this.dataSourceRingkasanPPM.paginator = this.paginatorRingkasanPpm
        this.dataSourceRingkasanPPM.sort = this.sortRingkasanPpm
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  getMitraPenelitian(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.getMitraPenelitian().subscribe(
      (data: GetMitraPenelitianResponse[]) => {
        this.isLoadingResults = false
        this.mitrapenelitis = data
        this.dataSourceMitraPenelitian.data = data
        this.dataSourceMitraPenelitian.paginator = this.paginatorMitraPenelitian
        this.dataSourceMitraPenelitian.sort = this.sortMitraPenelitian
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  getMitraPengabdian(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.getMitraPengabdian().subscribe(
      (data: GetMitraPengabdianResponse[]) => {
        this.isLoadingResults = false
        this.mitrapengabdis = data
        this.dataSourceMitraPengabdian.data = data
        this.dataSourceMitraPengabdian.paginator = this.paginatorMitraPengabdian
        this.dataSourceMitraPengabdian.sort = this.sortMitraPengabdian
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  //---------------------------------------------------------------------------
  // # Method POST
  addKegiatanPPM(ppm: AddPPMRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.addKegiatanPPM(ppm).subscribe(
      (data: MutatePPMResponse) => {
        this.isLoadingResults = false
        this.onCloseModalAdd()
        this.getPPMs()
        this.getPPMPegawais()
        this.getRingkasanPPM()
        this.getMitraPenelitian()
        this.getMitraPengabdian()
        this.toastrService.success(`Judul Penelitian ${ppm.judulPPM} berhasil ditambahkan`, 'success', {
          timeOut: 2500,
        })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }
  //addRelasiPegawaiPPM(ppm: AddRelasiRequest)

  addRelasiPegawaiPPM(ppm: AddRelasiRequest): void {
    // Cari nama dosen berdasarkan pegawaiId
    const selectedDosen = this.dosenSummary.find((dosen) => dosen.id === ppm.pegawaiID)
    const namaDosen = selectedDosen.nama
    //const selectedPpm = this.ppmSummary.find((ppms) => ppms.kegiatanID === ppm.kegiatanID);
    //const namaPpm = selectedPpm.judulPPM;
    // Tambahkan
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.addRelasiPegawaiPPM(ppm).subscribe(
      (data: MutatePPMResponse) => {
        this.isLoadingResults = false
        this.onCloseModalAdd()
        this.getPPMPegawais()
        this.getRingkasanPPM()
        this.getMitraPenelitian()
        this.getMitraPengabdian()
        //this.processPenelitianChartData()
        this.toastrService.success(`Dosen ${namaDosen} berhasil ditambah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  editKegiatanPPM(kegiatanId: string, ppm: EditPPMRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.ppmService.editKegiatanPPM(kegiatanId, ppm).subscribe(
      (data: MutatePPMResponse) => {
        this.isLoadingResults = false
        this.onCloseModalEdit()
        this.getPPMs()
        this.getPPMPegawais()
        this.getRingkasanPPM()
        this.getMitraPenelitian()
        this.getMitraPengabdian()
        this.toastrService.success(`Data Kegiatan PPM berhasil diubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  deleteKegiatanPPM(id: string): void {
    this.isLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.ppmService.deleteKegiatanPPM(id).subscribe(
            (data: MutatePPMResponse) => {
              this.isLoadingResults = false
              this.getPPMs()
              this.getPPMPegawais()
              this.getRingkasanPPM()
              this.getMitraPenelitian()
              this.getMitraPengabdian()
              this.toastrService.success
              this.toastrService.success('Kegiatan PPM berhasil dihapus!', 'success', { timeOut: 2500 })
            },
            (err: CommonError) => {
              err.messages.forEach((msg) => {
                this.toastrService.error(msg)
                this.isLoadingResults = false
              })
            }
          )
        }
      })
  }

  loadDosenSummary(): void {
    this.dosenService.getAllDosen().subscribe({
      next: (data) => {
        this.dosenSummary = data
      },
      error: (err) => {
        console.error('Gagal memuat data dosen:', err)
      },
    })
  }

  loadKegiatanPPMSummary(): void {
    this.ppmService.getAllPpm().subscribe({
      next: (data) => {
        this.ppmSummary = data
      },
      error: (err) => {
        console.error('Gagal memuat data Kegiatan PPM:', err)
      },
    })
  }
  /*
  processSimpleChartData(): void {
    const currentYear = new Date().getFullYear();
    const categories = ['Scopus', 'InternationalNonScopus', 'Sinta', 'HKI'];
  
    // Inisialisasi total jumlah per kategori
    const totals = categories.map((category) => ({ name: category, value: 0 }));
  
    // Hitung jumlah publikasi untuk setiap kategori dalam 5 tahun terakhir
    this.publikasis.forEach((publikasi) => {
      const year = publikasi.tahunPublikasi;
      const category = publikasi.kategoriPublikasi;
  
      if (
        year >= currentYear - 4 && // Tahun dalam 5 tahun terakhir
        year <= currentYear &&
        categories.includes(category) // Validasi kategori
      ) {
        const categoryData = totals.find((item) => item.name === category);
        if (categoryData) {
          categoryData.value += 1; // Tambahkan jumlah publikasi
        }
      }
    });
  
    // Tetapkan data ke properti simpleChartData
    this.simpleChartData = totals;
  
    console.log('Simple Chart Data:', this.simpleChartData); // Debugging
  }
  */
}
