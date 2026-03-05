import { Component, OnInit, ViewChild } from '@angular/core'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { BaseComponent } from 'src/app/base.component'
import { ToastrService } from 'ngx-toastr'
import { KepegawaianService } from '@core/services/kepegawaian.service'
import { CommonError } from '@core/error-handler/common-error'
import { FormGroup, NgForm } from '@angular/forms'
import {
  StatusDosen,
  StatusTendik,
  JabatanDosenTetap,
  Gender,
  Studi,
  Nikah,
  JenisP,
  DosenResponse,
  TendikResponse,
  MutatePegawaiResponse,
  PegawaiRequest,
  EditPegawaiRequest,
} from '@core/domain-classes/kepegawaian'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service'
import { TranslationService } from '@core/services/translation.service'

@Component({
  selector: 'kepegawaian',
  templateUrl: './kepegawaian.component.html',
  styleUrls: ['./kepegawaian.component.scss'],
})
export class KepegawaianComponent extends BaseComponent implements OnInit {
  dosens: DosenResponse[] = []
  tendiks: TendikResponse[] = []
  dataSourceDosen = new MatTableDataSource<DosenResponse>()
  dataSourceTendik = new MatTableDataSource<TendikResponse>()
  isLoadingResults = true
  jenisPegawai: string
  jenisDosen: string
  jenisTendik: string

  editIdTarget: string

  formAdd: FormGroup
  formEdit: FormGroup

  employee: EditPegawaiRequest = {
    id: '',
    nip: '',
    nama: '',
    tanggal_Lahir: '' as unknown as Date,
    tempat_Lahir: '',
    jenis_Kelamin: 2,
    alamat: '',
    telepon_Darurat: '',
    pendidikan_Terakhir: 5,
    gelar: '',
    status_Pernikahan: 4,
    nomor_BPJS: '',
    nomor_NPWP: '',
    nomor_Paspor: '',
    jenis_Dosen: 3,
    dosenTetap: {
      nidn: '',
      jabatan_Akademik: 4,
      golongan: '',
      tanggal_Pensiun: '' as unknown as Date,
      proyeksi: '',
      nomor_Serdos: '',
      tmt: '' as unknown as Date,
    },
    jenis_Tendik: 2,
    tendikTetap: {
      jabatan: '',
      golongan: '',
      tanggal_Pensiun: '' as unknown as Date,
      proyeksi: '',
      tmt: '' as unknown as Date,
    },
  }

  displayedColumnsDosen: string[] = [
    'nip',
    'nama',
    'status_kepegawaian',
    'tempat_lahir',
    'tanggal_lahir',
    'jenis_kelamin',
    'alamat',
    'telp_darurat',
    'pendidikan',
    'status_pernikahan',
    'nidn',
    'no_serdos',
    'gol_terakhir',
    'jab_fungsi_terakhir',
    'no_bpjs',
    'no_npwp',
    'no_paspor',
    'ubah',
    'hapus',
  ]

  displayedColumnsTendik: string[] = [
    'nip',
    'nama',
    'status_kepegawaian',
    'tempat_lahir',
    'tanggal_lahir',
    'jenis_kelamin',
    'alamat',
    'telp_darurat',
    'pendidikan',
    'status_pernikahan',
    'gol_terakhir',
    'jab_fungsi_terakhir',
    'no_bpjs',
    'no_npwp',
    'no_paspor',
    'ubah',
    'hapus',
  ]

  constructor(
    private commonDialogService: CommonDialogService,
    private kepegawaianService: KepegawaianService,
    private translationService: TranslationService,
    private toastrService: ToastrService
  ) {
    super()
  }

  @ViewChild('paginatorDosen', { static: true }) paginatorDosen: MatPaginator
  @ViewChild('paginatorTendik', { static: true }) paginatorTendik: MatPaginator

  @ViewChild(MatSort) sortDosen: MatSort
  @ViewChild(MatSort) sortTendik: MatSort

  exportToCSVDataDosen(): void {
    const header = [
      'NIP',
      'Nama',
      'Status Kepegawaian',
      'Tempat Lahir',
      'Jenis Kelamin',
      'Alamat',
      'Telepon Darurat',
      'Pendidikan',
      'Status Pernikahan',
      'Nomor BPJS',
      'Nomor NPWP',
      'Nomor Paspor',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.dataSourceDosen.data.map((row) => [
      row.nip,
      row.nama,
      row.jenis_Dosen,
      row.tempat_Lahir,
      row.jenis_Kelamin,
      row.alamat,
      row.telepon_Darurat,
      row.pendidikan_Terakhir,
      row.status_Pernikahan,
      row.nomor_BPJS,
      row.nomor_NPWP,
      row.nomor_Paspor,
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
    link.setAttribute('download', 'Informasi Dosen Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVDataTendik(): void {
    const header = [
      'NIP',
      'Nama',
      'Status Kepegawaian',
      'Tempat Lahir',
      'Jenis Kelamin',
      'Alamat',
      'Telepon Darurat',
      'Pendidikan',
      'Status Pernikahan',
      'Nomor BPJS',
      'Nomor NPWP',
      'Nomor Paspor',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.dataSourceTendik.data.map((row) => [
      row.nip,
      row.nama,
      row.jenis_Pegawai,
      row.tempat_Lahir,
      row.jenis_Kelamin,
      row.alamat,
      row.telepon_Darurat,
      row.pendidikan_Terakhir,
      row.status_Pernikahan,
      row.nomor_BPJS,
      row.nomor_NPWP,
      row.nomor_Paspor,
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
    link.setAttribute('download', 'Informasi Tendik Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  displayAdd = 'none'
  displayEdit = 'none'

  pageSizeOptionsDosen: number[] = [5, 10, 20]
  pageIndexDosen = 0
  pageSizeDosen = 5

  pageSizeOptionsTendik: number[] = [5, 10, 20]
  pageIndexTendik = 0
  pageSizeTendik = 5

  showXAxis = true
  showYAxis = true
  gradient = false
  showLegend = true
  showXAxisLabel = true
  showYAxisLabel = true
  xAxisLabelDosen = 'Proyeksi'
  yAxisLabelDosen = 'Jumlah'
  xAxisLabelTendik = 'Proyeksi'
  yAxisLabelTendik = 'Jumlah'
  proyeksiDosen: any[] = []
  proyeksiTendik: any[] = []

  ngOnInit(): void {
    this.getTendiks()
    this.getDosens()
  }

  onPageChangeDosen(event: any) {
    this.pageIndexDosen = event.pageIndex
    this.pageSizeDosen = event.pageSize
  }

  onPageChangeTendik(event: any) {
    this.pageIndexTendik = event.pageIndex
    this.pageSizeTendik = event.pageSize
  }

  formatDate(dateString: string): string {
    const date = new Date(dateString)
    const day = date.getUTCDate()
    const monthNames = [
      'Januari',
      'Februari',
      'Maret',
      'April',
      'Mei',
      'Juni',
      'Juli',
      'Agustus',
      'September',
      'Oktober',
      'November',
      'Desember',
    ]
    const month = monthNames[date.getUTCMonth()]
    const year = date.getUTCFullYear()

    return `${day} ${month} ${year}`
  }

  onSubmitAdd(form: NgForm) {
    console.log(form.value.jabatan_Akademik)
    if (form.valid) {
      const pegawai: PegawaiRequest = {
        nip: form.value.nip,
        nama: form.value.nama,
        tanggal_Lahir: form.value.tanggal_Lahir,
        tempat_Lahir: form.value.tempat_Lahir,
        jenis_Kelamin: Number(form.value.jenis_Kelamin),
        alamat: form.value.alamat,
        telepon_Darurat: form.value.telepon_Darurat,
        pendidikan_Terakhir: Number(form.value.pendidikan_Terakhir),
        gelar: form.value.gelar,
        status_Pernikahan: Number(form.value.status_Pernikahan),
        nomor_BPJS: form.value.nomor_BPJS,
        nomor_NPWP: form.value.nomor_NPWP,
        nomor_Paspor: form.value.nomor_Paspor,
        jenis_Pegawai: Number(form.value.jenis_Pegawai),
      }

      // Add optional fields based on the pegawai type
      if (form.value.jenis_Pegawai === '0') {
        // Dosen
        pegawai.jenis_Dosen = Number(form.value.jenis_Dosen)
        if (form.value.jenis_Dosen === '0' || form.value.jenis_Dosen === '1') {
          // Dosen PNS
          pegawai.dosenTetap = {
            nidn: form.value.nidn,
            jabatan_Akademik: Number(form.value.jabatan_Akademik),
            golongan: form.value.golongan,
            tmt: form.value.tmt,
            nomor_Serdos: form.value.nomor_Serdos,
          }
        }
      } else if (form.value.jenis_Pegawai === '1') {
        // Tendik
        pegawai.jenis_Tendik = Number(form.value.jenis_Tendik)
        if (form.value.jenis_Tendik === '0') {
          // Tendik PNS
          pegawai.tendikTetap = {
            jabatan: form.value.jabatan,
            golongan: form.value.golongan_Tendik,
            tmt: form.value.tmt_Tendik,
          }
        }
      }

      this.addPegawai(pegawai)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEdit(formEdit: NgForm) {
    if (formEdit.valid) {
      const pegawai: PegawaiRequest = {
        id: this.editIdTarget,
        nip: formEdit.value.nip_edit,
        nama: formEdit.value.nama_edit,
        tanggal_Lahir: formEdit.value.tanggal_Lahir_edit,
        tempat_Lahir: formEdit.value.tempat_Lahir_edit,
        jenis_Kelamin: Number(formEdit.value.jenis_Kelamin_edit),
        alamat: formEdit.value.alamat_edit,
        telepon_Darurat: formEdit.value.telepon_Darurat_edit,
        pendidikan_Terakhir: Number(formEdit.value.pendidikan_Terakhir_edit),
        gelar: formEdit.value.gelar_edit,
        status_Pernikahan: Number(formEdit.value.status_Pernikahan_edit),
        nomor_BPJS: formEdit.value.nomor_BPJS_edit,
        nomor_NPWP: formEdit.value.nomor_NPWP_edit,
        nomor_Paspor: formEdit.value.nomor_Paspor_edit,
        jenis_Pegawai: Number(formEdit.value.jenis_Pegawai_edit),
      }

      // Check for conditional fields based on jenisPegawai and jenisDosen/jenisTendik
      if (formEdit.value.jenis_Pegawai === '0') {
        // Dosen
        pegawai.jenis_Dosen = Number(formEdit.value.jenis_Dosen_edit)
        if (formEdit.value.jenis_Dosen_edit === '0') {
          // Dosen PNS
          pegawai.dosenTetap = {
            nidn: formEdit.value.nidn_edit,
            jabatan_Akademik: Number(formEdit.value.jabatan_Akademik_edit),
            golongan: formEdit.value.golongan_edit,
            tmt: formEdit.value.tmt_edit,
            nomor_Serdos: formEdit.value.nomor_Serdos_edit,
          }
        }
      } else if (formEdit.value.jenis_Pegawai_edit === '1') {
        // Tendik
        pegawai.jenis_Tendik = Number(formEdit.value.jenis_Tendik_edit)
        if (formEdit.value.jenis_Tendik_edit === '0') {
          // Tendik PNS
          pegawai.tendikTetap = {
            jabatan: formEdit.value.jabatan_edit,
            golongan: formEdit.value.golongan_Tendik_edit,
            tmt: formEdit.value.tmt_Tendik_edit,
          }
        }
      }

      // Proceed with submission (e.g., call a service to send the data)
      console.log('Submitted pegawai data:', pegawai)

      // Example call to a service
      this.editPegawai(this.editIdTarget, pegawai)
    }
  }

  resetForm(form?: any): void {
    if (form) {
      form.resetForm()
    }
  }

  openModalAdd() {
    this.resetForm()
    this.displayAdd = 'block'
  }

  onCloseModalAdd() {
    this.displayAdd = 'none'
  }

  openModalEdit(id: string) {
    console.log(id)
    this.editIdTarget = id
    this.displayEdit = 'block'
  }

  onCloseModalEdit() {
    this.displayEdit = 'none'
  }

  transformDosensData(dosens: DosenResponse[]): any[] {
    let aktifCount = 0
    let pensiunCount = 0
    let lainnyaCount = 0

    dosens.forEach((dosen) => {
      const proyeksi = dosen?.dosenTetap?.proyeksi

      if (proyeksi === 'Aktif') {
        aktifCount++
      } else if (proyeksi === 'Pensiun') {
        pensiunCount++
      } else {
        lainnyaCount++
      }
    })

    return [
      { name: 'Aktif', value: aktifCount },
      { name: 'Pensiun', value: pensiunCount },
      { name: 'Lainnya', value: lainnyaCount },
    ]
  }

  transformTendiksData(tendiks: TendikResponse[]): any[] {
    let aktifCount = 0
    let pensiunCount = 0
    let lainnyaCount = 0

    tendiks.forEach((tendik) => {
      const proyeksi = tendik?.tendikTetap?.proyeksi

      if (proyeksi === 'Aktif') {
        aktifCount++
      } else if (proyeksi === 'Pensiun') {
        pensiunCount++
      } else {
        lainnyaCount++
      }
    })

    return [
      { name: 'Aktif', value: aktifCount },
      { name: 'Pensiun', value: pensiunCount },
      { name: 'Lainnya', value: lainnyaCount },
    ]
  }

  getStatusDosenFromInt(value: number): StatusDosen {
    if (value in StatusDosen) {
      return StatusDosen[value as unknown as keyof typeof StatusDosen]
    }
    throw new Error(`Invalid StatusDosen value: ${value}`)
  }

  getStatusTendikFromInt(value: number): StatusTendik {
    if (value in StatusTendik) {
      return StatusTendik[value as unknown as keyof typeof StatusTendik]
    }
    throw new Error(`Invalid StatusTendik value: ${value}`)
  }

  getJabatanDosenTetapFromInt(value: number): JabatanDosenTetap {
    if (value in JabatanDosenTetap) {
      return JabatanDosenTetap[value as unknown as keyof typeof JabatanDosenTetap]
    }
    throw new Error(`Invalid JabatanDosenTetap value: ${value}`)
  }

  getGenderFromInt(value: number): Gender {
    if (value in Gender) {
      return Gender[value as unknown as keyof typeof Gender]
    }
    throw new Error(`Invalid Gender value: ${value}`)
  }

  getStudiFromInt(value: number): Studi {
    if (value in Studi) {
      return Studi[value as unknown as keyof typeof Studi]
    }
    throw new Error(`Invalid Studi value: ${value}`)
  }

  getNikahFromInt(value: number): Nikah {
    if (value in Nikah) {
      return Nikah[value as unknown as keyof typeof Nikah]
    }
    throw new Error(`Invalid Nikah value: ${value}`)
  }

  getJenisPFromInt(value: number): JenisP {
    if (value in JenisP) {
      return JenisP[value as unknown as keyof typeof JenisP]
    }
    throw new Error(`Invalid JenisP value: ${value}`)
  }

  addSpaceBeforeUppercase(str: string): string {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2')
  }

  // API CALL
  getDosens(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kepegawaianService.getDosens().subscribe(
      (data: DosenResponse[]) => {
        this.isLoadingResults = false
        this.dosens = data
        this.proyeksiDosen = this.transformDosensData(data)
        this.dataSourceDosen.data = data
        this.dataSourceDosen.paginator = this.paginatorDosen
        this.dataSourceDosen.sort = this.sortDosen
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  getTendiks(): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kepegawaianService.getTendiks().subscribe(
      (data: TendikResponse[]) => {
        this.isLoadingResults = false
        this.tendiks = data
        this.proyeksiTendik = this.transformTendiksData(data)
        this.dataSourceTendik.data = data
        this.dataSourceTendik.paginator = this.paginatorTendik
        this.dataSourceTendik.sort = this.sortTendik
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  addPegawai(pegawai: PegawaiRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kepegawaianService.addPegawai(pegawai).subscribe(
      (data: MutatePegawaiResponse) => {
        this.isLoadingResults = false
        this.onCloseModalAdd()
        this.getDosens()
        this.getTendiks()
        this.toastrService.success(`Pengguna ${pegawai.nama} berhasil ditambah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  editPegawai(id: string, pegawai: PegawaiRequest): void {
    this.isLoadingResults = true
    this.sub$.sink = this.kepegawaianService.editPegawai(id, pegawai).subscribe(
      (data: MutatePegawaiResponse) => {
        this.isLoadingResults = false
        this.onCloseModalEdit()
        this.getDosens()
        this.getTendiks()
        this.toastrService.success(`Data pengguna berhasil dirubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isLoadingResults = false
        })
      }
    )
  }

  deletePegawai(name: string, id: string): void {
    this.isLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')} ${name}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.kepegawaianService.deletePegawai(id).subscribe(
            (data: MutatePegawaiResponse) => {
              this.isLoadingResults = false
              this.getDosens()
              this.getTendiks()
              this.toastrService.success('Pengguna berhasil terhapus!', 'success', { timeOut: 2500 })
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
