import { Component, OnInit, ViewChild } from '@angular/core'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { BaseComponent } from 'src/app/base.component'
import { ToastrService } from 'ngx-toastr'
import { KependidikanService } from '@core/services/pendidikan.service'
import { CommonError } from '@core/error-handler/common-error'
import { FormGroup, FormBuilder, NgForm } from '@angular/forms'
import {
  MutatePendidikanResponse,
  PendidikanResponse,
  MutatePendidikanRequest,
  MutatePendidikanEditRequest,
  MutatePendidikanDeleteRequest,
} from '@core/domain-classes/pendidikan'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service'
import { TranslationService } from '@core/services/translation.service'

@Component({
    selector: 'pendidikan',
    templateUrl: './pendidikan.component.html',
    styleUrls: ['./pendidikan.component.scss'],
    standalone: false
})
export class PendidikanComponent extends BaseComponent implements OnInit {
  kependidikans: PendidikanResponse[] = []
  isLoadingResults = false
  dataSourceKependidikan = new MatTableDataSource<PendidikanResponse>()
  currentYear: number = new Date().getFullYear()

  formAdd: FormGroup
  formEdit: FormGroup

  displayedColumsKependidikan: string[] = [
    'thn_akademik',
    'semester',
    'deskripsi',
    'thn_masuk',
    'total_registrasi_mhs',
    'mahasiswa_aktif',
    'mahasiswa_lulus',
    'mahasiswa_non_aktif',
    'mahasiswa_do',
    'mahasiswa_undur_diri',
    'rataan_ipk_total',
    'jumlah_ipk_dibawah_2',
    'masa_studi_dibawah_8',
    'masa_studi_8_sampai_10',
    'masa_studi_diatas_10',
    'ubah',
    'hapus',
  ]

  constructor(
    private commonDialogService: CommonDialogService,
    private kependidikanService: KependidikanService,
    private translationService: TranslationService,
    private toastrService: ToastrService
  ) {
    super()
  }

  @ViewChild(MatSort) sortKependidikan: MatSort
  @ViewChild('paginatorKependidikan', { static: true }) paginatorKependidikan: MatPaginator

  displayAdd = 'none'
  displayEdit = 'none'

  pageSizeOptionsKependidikan: number[] = [5, 10, 20]
  pageIndexKependidikan = 0
  pageSizeKependidikan = 5

  showXAxis = true
  showYAxis = true
  gradient = false
  showLegend = true
  showXAxisLabel = true
  showYAxisLabel = true
  xAxisLabelKependidikan = `Mahasiswa tahun masuk ${this.currentYear - 4}`
  yAxisLabelKependidikan = 'Jumlah'
  proyeksiKependidikan: any[] = []

  ngOnInit(): void {
    this.getKependidikans()
  }

  exportToCSVDataKependidikan(): void {
    const header = [
      'Tahun Akademik',
      'thn_masuk',
      'total_registrasi_mhs',
      'mahasiswa_aktif',
      'mahasiswa_lulus',
      'mahasiswa_non_aktif',
      'mahasiswa_do',
      'mahasiswa_undur_diri',
      'rataan_ipk_total',
      'jumlah_ipk_dibawah_2',
      'masa_studi_dibawah_8',
      'masa_studi_8_sampai_10',
      'masa_studi_diatas_10',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.dataSourceKependidikan.data.map((row) => [
      row.deskripsi,
      row.thnMasuk,
      row.totalRegistrasiMhs,
      row.infoMahsiswa.jmlhAktif,
      row.infoMahsiswa.jmlhLulus,
      row.infoMahsiswa.jmlhNonAktif,
      row.infoMahsiswa.jmlhDO,
      row.infoMahsiswa.jmlhUndurDiri,
      row.infoMahsiswa.rataanIPKTotal,
      row.infoMahsiswa.jumlahIPKDibawah2,
      row.infoMahsiswa.masaStudiDibawah8,
      row.infoMahsiswa.masaStudi8Sampai10,
      row.infoMahsiswa.masaStudiDiatas10,
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
    link.setAttribute('download', 'Informasi Kependidikan Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  onPageChangeKependidikan(event: any) {
    this.pageIndexKependidikan = event.pageIndex
    this.pageSizeKependidikan = event.pageSize
  }

  openModalAdd() {
    this.displayAdd = 'block'
  }

  onCloseModalAdd() {
    this.displayAdd = 'none'
  }

  openModalEdit() {
    this.displayEdit = 'block'
  }

  onCloseModalEdit() {
    this.displayEdit = 'none'
  }

  transformsKependidikanData(kependidikans: PendidikanResponse[]) {
    let aktif = 0
    let lulus = 0
    let nonAktif = 0
    let dropOut = 0
    let undurDiri = 0

    kependidikans.forEach((kependidikan) => {
      const infoMhs = kependidikan.infoMahsiswa

      if (kependidikan.thnMasuk === this.currentYear - 4) {
        aktif += kependidikan.infoMahsiswa.jmlhAktif
        lulus += kependidikan.infoMahsiswa.jmlhLulus
        nonAktif += kependidikan.infoMahsiswa.jmlhNonAktif
        dropOut += kependidikan.infoMahsiswa.jmlhDO
        undurDiri += kependidikan.infoMahsiswa.jmlhUndurDiri
      }
    })

    console.log([
      { name: 'Aktif', value: aktif },
      { name: 'Lulus', value: lulus },
      { name: 'Non-aktif', value: nonAktif },
      { name: 'Drop Out', value: dropOut },
      { name: 'Undur Diri', value: undurDiri },
    ])

    return [
      { name: 'Aktif', value: aktif },
      { name: 'Lulus', value: lulus },
      { name: 'Non-aktif', value: nonAktif },
      { name: 'Drop Out', value: dropOut },
      { name: 'Undur Diri', value: undurDiri },
    ]
  }

  onSubmitAdd(form: NgForm) {
    if (form.valid) {
      console.log(form.value)
      this.addKependidikan(form.value)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEdit(form: NgForm) {
    if (form.valid) {
      console.log(form.value)
      this.editKependidikan(form.value)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onClickDelete(thnMasukId: number) {
    if (thnMasukId) {
      this.deleteKependidikan(thnMasukId)
    } else {
      this.toastrService.error('Tidak bisa menghapus data!', 'Error!', { timeOut: 2500 })
    }
  }

  // API CALL
  getKependidikans(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kependidikanService.getKependidikans().subscribe(
      (data: PendidikanResponse[]) => {
        this.isLoadingResults = false
        this.kependidikans = data
        this.proyeksiKependidikan = this.transformsKependidikanData(data)
        this.dataSourceKependidikan.data = data
        this.dataSourceKependidikan.paginator = this.paginatorKependidikan
        this.dataSourceKependidikan.sort = this.sortKependidikan
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  addKependidikan(kependidikan: MutatePendidikanRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kependidikanService.addKependidikan(kependidikan).subscribe(
      (data: MutatePendidikanResponse) => {
        this.isLoadingResults = false
        this.onCloseModalAdd()
        this.getKependidikans()
        this.toastrService.success(`Kependidikan berhasil ditambah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  editKependidikan(kependidikan: MutatePendidikanEditRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kependidikanService.editKependidikan(kependidikan).subscribe(
      (data: MutatePendidikanResponse) => {
        this.isLoadingResults = false
        this.onCloseModalEdit()
        this.getKependidikans()
        this.toastrService.success(`Kependidikan berhasil diubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  deleteKependidikan(thnMasukId: number): void {
    this.isLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.kependidikanService.deleteKependidikan(thnMasukId).subscribe(
            (data: MutatePendidikanResponse) => {
              this.isLoadingResults = false
              this.getKependidikans()
              this.toastrService.success(`Kependidikan berhasil diubah`, 'success', { timeOut: 2500 })
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
}
