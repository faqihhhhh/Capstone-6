import { Component, OnInit, ViewChild } from '@angular/core'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { BaseComponent } from 'src/app/base.component'
import { ToastrService } from 'ngx-toastr'
import { PublikasiService } from '@core/services/publikasi.service'
import { KepegawaianService } from '@core/services/kepegawaian.service'
import { CommonError } from '@core/error-handler/common-error'
import { FormGroup, FormBuilder, NgForm } from '@angular/forms'
import {
  KategoriPubs,
  PenerapanMasy,
  GetPublikasiResponse,
  GetRingkasanPublikasiResponse,
  PostPublikasiRequest,
  EditPublikasiRequest,
  MutatePublikasiResponse,
} from '@core/domain-classes/publikasi'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service'
import { TranslationService } from '@core/services/translation.service'

@Component({
    selector: 'publikasi',
    templateUrl: './publikasi.component.html',
    styleUrls: ['./publikasi.component.scss'],
    standalone: false
})
export class PublikasiComponent extends BaseComponent implements OnInit {
  publikasis: GetPublikasiResponse[] = []
  ringkasans: GetRingkasanPublikasiResponse[] = []
  dataSourcePublikasi = new MatTableDataSource<GetPublikasiResponse>()
  dataSourceRingkasanPublikasi = new MatTableDataSource<GetRingkasanPublikasiResponse>()
  isLoadingResults = true
  dosenSummary: { id: string; nama: string; nip: string }[] = []
  selectedDosenId: string | null = null
  editIdTarget: string
  simpleChartData: { name: string; value: number }[] = []

  formAdd: FormGroup
  formEdit: FormGroup

  displayedColumnsPublikasi: string[] = [
    'nip',
    'nama',
    'tahunPublikasi',
    'judulPublikasi',
    'kategoriPublikasi',
    'penerapanMasyarakat',
    'ubah',
    'hapus',
  ]

  displayedColumnsRingkasanPublikasi: string[] = [
    'nip',
    'nama',
    'totalScopus',
    'totalNonScopus',
    'totalSinta',
    'totalHKI',
    'totalPenerapanMasyarakat',
  ]

  constructor(
    private commonDialogService: CommonDialogService,
    private dosenService: KepegawaianService,
    private publikasiService: PublikasiService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ) {
    super()
  }

  @ViewChild('paginatorPublikasi', { static: true }) paginatorPublikasi: MatPaginator
  @ViewChild('paginatorRingkasan', { static: true }) paginatorRingkasanPublikasi: MatPaginator

  @ViewChild(MatSort) sortPublikasi: MatSort
  @ViewChild(MatSort) sortRingkasanPublikasi: MatSort

  displayAdd = 'none'
  displayEdit = 'none'

  pageSizeOptionsPublikasi: number[] = [5, 10, 20]
  pageIndexPublikasi = 0
  pageSizePublikasi = 5

  pageSizeOptionsRingkasanPublikasi: number[] = [5, 10, 20]
  pageIndexRingkasanPublikasi = 0
  pageSizeRingkasanPublikasi = 5

  ngOnInit(): void {
    this.loadDosenSummary()
    this.getPublikasis()
    this.getRingkasans()
  }

  onPageChangePublikasi(event: any) {
    this.pageIndexPublikasi = event.pageIndex
    this.pageSizePublikasi = event.pageSize
  }

  onPageChangeRingkasanPublikasi(event: any) {
    this.pageIndexRingkasanPublikasi = event.pageIndex
    this.pageSizeRingkasanPublikasi = event.pageSize
  }

  onSubmitAdd(form: NgForm) {
    console.log(form)
    if (form.valid) {
      const publikasi: PostPublikasiRequest = {
        pegawaiId: form.value.dosen,
        tahunPublikasi: form.value.tahunPublikasi,
        judulPublikasi: form.value.judulPublikasi,
        //kategoriPublikasi: Number(form.value.kategoriPublikasi),
        kategoriPublikasi: form.value.kategoriPublikasi,
        penerapanMasyarakat: form.value.penerapanMasyarakat,
      }
      this.addPublikasi(publikasi)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEdit(formEdit: NgForm) {
    if (!this.editIdTarget) {
      this.toastrService.error('Publikasi ID tidak ditemukan!', 'Error!', { timeOut: 2500 })
      return
    }
    console.log(formEdit)
    if (formEdit.valid) {
      const publikasi: EditPublikasiRequest = {
        tahunPublikasi: formEdit.value.tahunPublikasi_edit,
        judulPublikasi: formEdit.value.judulPublikasi_edit,
        //kategoriPublikasi: Number(formEdit.value.kategoriPublikasi_edit),
        kategoriPublikasi: formEdit.value.kategoriPublikasi_edit,
        penerapanMasyarakat: formEdit.value.penerapanMasyarakat_edit,
      }

      // Proceed with submission (e.g., call a service to send the data)
      console.log('Submitted Data Publikasi:', publikasi)

      // Example call to a service
      this.editPublikasi(this.editIdTarget, publikasi)
    }
  }

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

  /*
  getKategoriPubsFromInt(value: number): KategoriPubs {
    if (value in KategoriPubs) {
      return KategoriPubs[value as unknown as keyof typeof KategoriPubs]
    }
    throw new Error(`Invalid StatusDosen value: ${value}`)
  } 

  getPenerapanMasyFromInt(value: number): PenerapanMasy {
    if (value in PenerapanMasy) {
      return PenerapanMasy[value as unknown as keyof typeof PenerapanMasy]
    }
    throw new Error(`Invalid StatusTendik value: ${value}`)
  }
  */

  processSimpleChartData(): void {
    const currentYear = new Date().getFullYear()
    const categories = ['Scopus', 'InternationalNonScopus', 'Sinta', 'HKI']

    // Inisialisasi total jumlah per kategori
    const totals = categories.map((category) => ({ name: category, value: 0 }))

    // Hitung jumlah publikasi untuk setiap kategori dalam 5 tahun terakhir
    this.publikasis.forEach((publikasi) => {
      const year = publikasi.tahunPublikasi
      const category = publikasi.kategoriPublikasi

      if (
        year >= currentYear - 4 && // Tahun dalam 5 tahun terakhir
        year <= currentYear &&
        categories.includes(category) // Validasi kategori
      ) {
        const categoryData = totals.find((item) => item.name === category)
        if (categoryData) {
          categoryData.value += 1 // Tambahkan jumlah publikasi
        }
      }
    })

    // Tetapkan data ke properti simpleChartData
    this.simpleChartData = totals

    console.log('Simple Chart Data:', this.simpleChartData) // Debugging
  }

  mapKategoriPublikasi(value: string): KategoriPubs {
    if (Object.values(KategoriPubs).includes(value as KategoriPubs)) {
      return value as KategoriPubs // Casting ke enum jika valid
    }
    throw new Error(`Invalid kategoriPublikasi value: ${value}`)
  }

  getKategoriPubsFromInt(value: string): string {
    // Periksa apakah value cocok dengan salah satu nilai enum
    if (Object.values(KategoriPubs).includes(value as KategoriPubs)) {
      return value // Jika cocok, kembalikan value langsung
    }
    return 'Tidak Diketahui' // Jika tidak cocok, kembalikan "Tidak Diketahui"
  }

  getPenerapanMasyFromInt(value: number): string {
    return PenerapanMasy[value] || 'Tidak Diketahui'
  }
  exportToCSVPublikasi(): void {
    const header = ['NIP', 'Nama', 'Tahun Publikasi', 'Judul Publikasi', 'Kategori Publikasi', 'Penerapan Masyarakat']

    // Ambil data dari dataSourcePublikasi
    const rows = this.dataSourcePublikasi.data.map((row) => [
      row.nip,
      row.nama,
      row.tahunPublikasi,
      row.judulPublikasi,
      row.kategoriPublikasi,
      row.penerapanMasyarakat,
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
    link.setAttribute('download', 'Daftar Publikasi Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVRingkasanPublikasi(): void {
    const header = [
      'NIP',
      'Nama',
      'Jumlah Publikasi Scopus',
      'Jumlah Publikasi Non-Scopus',
      'Jumlah Publikasi Sinta',
      'Jumlah HKI',
      'Jumlah Penerapan Masyarakat',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.dataSourceRingkasanPublikasi.data.map((row) => [
      row.nip,
      row.nama,
      row.totalScopus,
      row.totalNonScopus,
      row.totalSinta,
      row.totalHKI,
      row.totalPenerapanMasyarakat,
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
    link.setAttribute('download', 'Ringkasan Publikasi Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  addSpaceBeforeUppercase(str: string): string {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2')
  }

  /*
  API CALL melalui publikasi.service
  ada 5 endpoint:
  1. getPublikasis() --> tidak ada parameter dan untuk datanya adalah GetPublikasiResponse
  2. getRingkasans() --> tidak ada parameter dan untuk datanya adalah GetRingkasanPublikasiResponse
  3. addPublikasi(publikasi: PostPublikasiRequest) --> parameter dan datanya adalah PostPublikasiRequest
  4. editPublikasi(publikasiId: string, publikasi: EditPublikasiRequest) --> parameter dan dantanya adalah publikasiId dan EditPublikasiRequest
  5. deletePublikasi(publikasiId: string) --> tidak ada data, parameternya adalah publikasiId
  */
  getPublikasis(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.publikasiService.getPublikasis().subscribe(
      (data: GetPublikasiResponse[]) => {
        this.isLoadingResults = false

        // Validasi dan map kategoriPublikasi ke enum
        this.publikasis = data.map((publikasi) => ({
          ...publikasi,
          kategoriPublikasi: this.mapKategoriPublikasi(publikasi.kategoriPublikasi),
        }))

        this.dataSourcePublikasi.data = this.publikasis
        this.dataSourcePublikasi.paginator = this.paginatorPublikasi
        this.dataSourcePublikasi.sort = this.sortPublikasi

        this.processSimpleChartData()
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  getRingkasans(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.publikasiService.getRingkasans().subscribe(
      (data: GetRingkasanPublikasiResponse[]) => {
        this.isLoadingResults = false
        this.ringkasans = data
        //this.proyeksiTendik = this.transformTendiksData(data)
        this.dataSourceRingkasanPublikasi.data = data
        this.dataSourceRingkasanPublikasi.paginator = this.paginatorRingkasanPublikasi
        this.dataSourceRingkasanPublikasi.sort = this.sortRingkasanPublikasi
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  addPublikasi(publikasi: PostPublikasiRequest): void {
    // Cari nama dosen berdasarkan pegawaiId
    const selectedDosen = this.dosenSummary.find((dosen) => dosen.id === publikasi.pegawaiId)
    const namaDosen = selectedDosen.nama
    // Tambahkan
    this.isLoadingResults = true
    this.sub$.sink = this.publikasiService.addPublikasi(publikasi).subscribe(
      (data: MutatePublikasiResponse) => {
        this.isLoadingResults = false
        this.onCloseModalAdd()
        this.getPublikasis()
        this.getRingkasans()
        this.toastrService.success(`Publikasi dengan nama Dosen ${namaDosen} berhasil ditambah`, 'success', {
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

  editPublikasi(id: string, publikasi: EditPublikasiRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.publikasiService.editPublikasi(id, publikasi).subscribe(
      (data: MutatePublikasiResponse) => {
        this.isLoadingResults = false
        this.onCloseModalEdit()
        this.getPublikasis()
        this.getRingkasans()
        this.toastrService.success(`Data Publikasi berhasil diubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  deletePublikasi(id: string): void {
    this.isLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.publikasiService.deletePublikasi(id).subscribe(
            (data: MutatePublikasiResponse) => {
              this.isLoadingResults = false
              this.getPublikasis()
              this.getRingkasans()
              this.toastrService.success('Publikasi berhasil dihapus!', 'success', { timeOut: 2500 })
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
}
