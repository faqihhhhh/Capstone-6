import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { BaseComponent } from 'src/app/base.component'
import { KemahasiswaanService } from '@core/services/kemahasiswaan.service'
import {
  KemahasiswaanPrestasiData,
  KemahasiswaanPrestasiRequest,
  KemahasiswaanTracerData,
  KemahasiswaanTracerRequest,
  MutateKemahasiswaanPrestasiResponse,
  MutateKemahasiswaanTracerResponse,
  MutateStatusLulusan,
  PrestasiMhs,
  StatusLulusan,
} from '@core/domain-classes/kemahasiswaan'
import { FormGroup, NgForm } from '@angular/forms'
import { ToastrService } from 'ngx-toastr'
import { CommonError } from '@core/error-handler/common-error'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service'
import { TranslationService } from '@core/services/translation.service'

@Component({
  selector: 'kemahasiswaan',
  templateUrl: './kemahasiswaan.component.html',
  styleUrls: ['./kemahasiswaan.component.scss'],
})
export class KemahasiswaanComponent extends BaseComponent implements OnInit {
  prestasis: KemahasiswaanPrestasiData[] = []
  tracers: StatusLulusan[] = []
  dataSourcePrestasi = new MatTableDataSource<KemahasiswaanPrestasiData>()
  dataSourceTracer = new MatTableDataSource<StatusLulusan>()
  currentYear: number = new Date().getFullYear()

  isPrestasiLoadingResults = false
  isTracerLoadingResults = false

  editTracerIdTarget: string
  editPrestasiIdTarget: string

  formAddPrestasi: FormGroup
  formEditPrestasi: FormGroup
  formAddTracer: FormGroup
  formEditTracer: FormGroup

  displayedColumnPrestasi: string[] = [
    'tahun_akademik',
    'semester',
    'pkm',
    'mapres',
    'lomba_nasional',
    'lomba_internasional',
    'inbound',
    'outbond',
    'ubah',
    'hapus',
  ]

  displayedColumnTracer: string[] = [
    'tahun_lulus',
    'tahun_input',
    'presentase_bekerja',
    'presentase_lanjutStudi',
    'presentase_internship',
    'presentase_wirausaha',
    'presentase_belumKerja',
    'presentase_masaTungguKerja_diatas6Bulan',
    'presentase_masaTungguKerja_dibawah6bulan',
    'presentase_founder',
    'presentase_cofounder',
    'presentase_staff',
    'presentase_freelancer',
    'presentase_bumn',
    'presentase_organisasi_multilateral',
    'presentase_instansi_pemerintah',
    'presentase_organisasi_nonprofit',
    'presentase_di_wirausaha',
    'presentase_lainnya',
    'presentase_lokal',
    'presentase_nasional',
    'presentase_multinasional',
    'ubah',
    'hapus',
  ]

  constructor(
    private commonDialogService: CommonDialogService,
    private kemahasiswaanService: KemahasiswaanService,
    private translationService: TranslationService,
    private toastrService: ToastrService
  ) {
    super()
  }

  @ViewChild('paginatorTracer', { static: true }) paginatorTracer: MatPaginator
  @ViewChild('paginatorPrestasi', { static: true }) paginatorPrestasi: MatPaginator

  @ViewChild(MatSort) sortTracer: MatSort
  @ViewChild(MatSort) sortPrestasi: MatSort

  displayAddTracer = 'none'
  displayEditTracer = 'none'
  displayAddPrestasi = 'none'
  displayEditPrestasi = 'none'

  pageSizeOptionsTracer: number[] = [5, 10, 20]
  pageIndexTracer = 0
  pageSizeTracer = 5

  pageSizeOptionsPrestasi: number[] = [5, 10, 20]
  pageIndexPrestasi = 0
  pageSizePrestasi = 10

  showXAxis = true
  showYAxis = true
  gradient = false
  showLegend = true
  showXAxisLabel = true
  showYAxisLabel = true
  xAxisLabelPrestasi = `Prestasi Tahun Akademik ${this.currentYear}`
  yAxisLabelPrestasi = 'Jumlah'
  xAxisLabelTracer = `Tracer Tahun Lulus ${this.currentYear - 5}`
  yAxisLabelTracer = 'Jumlah'
  proyeksiPrestasi: any[] = []
  proyeksiTracer: any[] = []

  ngOnInit(): void {
    this.getTracers()
    this.getPrestasis()
  }

  onPageChangeTracer(event: any) {
    this.pageIndexTracer = event.pageIndex
    this.pageSizeTracer = event.pageSize
  }

  onPageChangePrestasi(event: any) {
    this.pageIndexPrestasi = event.pageIndex
    this.pageSizePrestasi = event.pageSize
  }

  onSubmitAddPrestasi(form: NgForm) {
    if (form.valid) {
      const prestasi: KemahasiswaanPrestasiRequest = {
        thnAkademik: form.value.tahunAkademik,
        semester: form.value.semester,
        prestasiMhs: {
          jmlhPKM: form.value.pkm,
          jumlhMapres: form.value.mapres,
          jumlhLombaNasional: form.value.lombaNasional,
          jumlhLombaInter: form.value.lombaInternasional,
          jumlhInbound: form.value.inbound,
          jumlhOutbound: form.value.outbound,
        },
      }

      this.addPrestasi(prestasi)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEditPrestasi(form: NgForm) {
    if (form.valid) {
      const prestasi: PrestasiMhs = {
        // thnAkademik: form.value.tahunAkademik,
        // semester: form.value.semester,
        // prestasiMhs: {
        //   jmlhPKM: form.value.pkm,
        //   jumlhMapres: form.value.mapres,
        //   jumlhLombaNasional: form.value.lombaNasional,
        //   jumlhLombaInter: form.value.lombaInternasional,
        //   jumlhInbound: form.value.inbound,
        //   jumlhOutbound: form.value.outbound,
        // },
        jmlhPKM: form.value.pkm,
        jumlhMapres: form.value.mapres,
        jumlhLombaNasional: form.value.lombaNasional,
        jumlhLombaInter: form.value.lombaInternasional,
        jumlhInbound: form.value.inbound,
        jumlhOutbound: form.value.outbound,
      }

      this.editPrestasi(this.editPrestasiIdTarget, prestasi)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitAddTracer(form: NgForm) {
    if (form.valid) {
      console.log(form.value)
      const tracer: KemahasiswaanTracerRequest = {
        lulusanTahun: form.value.tahunLulus,
        statusLulusan: [
          {
            tahun_Input: form.value.tahunInput,
            bekerja: form.value.bekerja,
            lanjutStudi: form.value.lanjutStudi,
            internship: form.value.internship,
            berwirausaha: form.value.berwirausaha,
            belumKerja: form.value.belumKerja,
            masaTungguKerja: {
              diatas6Bulan: form.value.masaTungguKerjaDiatas6Bulan,
              dibawah6Bulan: form.value.masaTungguKerjaDibawah6bulan,
            },
            jenisTempatKerja: {
              bumn: form.value.bumn,
              organisasi_Multilateral: form.value.organisasi_multilateral,
              instansi_Pemerintah: form.value.instansi_pemerintah,
              organisasi_NonProfit: form.value.organisasi_nonprofit,
              wirausaha: form.value.wirausaha,
              lainnya: form.value.lainnya,
            },
            posisiJabatan: {
              founder: form.value.founder,
              coFounder: form.value.cofounder,
              staf: form.value.staff,
              freelancer: form.value.freelancer,
            },
            tingkatTempatKerja: {
              lokal: form.value.lokal,
              nasional: form.value.nasional,
              multiNasional: form.value.multinasional,
            },
          },
        ],
      }

      this.addTracer(tracer)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  onSubmitEditTracer(form: NgForm) {
    if (form.valid) {
      console.log(form.value)
      const tracer: MutateStatusLulusan = {
        bekerja: form.value.bekerja,
        lanjutStudi: form.value.lanjutStudi,
        internship: form.value.internship,
        berwirausaha: form.value.berwirausaha,
        belumKerja: form.value.belumKerja,
        masaTungguKerja: {
          diatas6Bulan: form.value.masaTungguKerjaDiatas6Bulan,
          dibawah6Bulan: form.value.masaTungguKerjaDibawah6bulan,
        },
        jenisTempatKerja: {
          bumn: form.value.bumn,
          organisasi_Multilateral: form.value.organisasi_multilateral,
          instansi_Pemerintah: form.value.instansi_pemerintah,
          organisasi_NonProfit: form.value.organisasi_nonprofit,
          wirausaha: form.value.wirausaha,
          lainnya: form.value.lainnya,
        },
        posisiJabatan: {
          founder: form.value.founder,
          coFounder: form.value.cofounder,
          staf: form.value.staff,
          freelancer: form.value.freelancer,
        },
        tingkatTempatKerja: {
          lokal: form.value.lokal,
          nasional: form.value.nasional,
          multiNasional: form.value.multinasional,
        },
      }

      console.log(tracer)

      this.editTracer(this.editTracerIdTarget, tracer)
    } else {
      this.toastrService.error('Isi data dengan benar!', 'Error!', { timeOut: 2500 })
    }
  }

  openModalAddTracer() {
    this.displayAddTracer = 'block'
  }

  onCloseModalAddTracer() {
    this.displayAddTracer = 'none'
  }

  openModalEditTracer(id: string) {
    console.log(id)
    this.editTracerIdTarget = id
    this.displayEditTracer = 'block'
  }

  onCloseModalEditTracer() {
    this.displayEditTracer = 'none'
  }

  openModalAddPrestasi() {
    this.displayAddPrestasi = 'block'
  }

  onCloseModalAddPrestasi() {
    this.displayAddPrestasi = 'none'
  }

  openModalEditPrestasi(id: string) {
    console.log(id)
    this.editPrestasiIdTarget = id
    this.displayEditPrestasi = 'block'
  }

  onCloseModalEditPrestasi() {
    this.displayEditPrestasi = 'none'
  }

  transformsPrestasiData(prestasis: KemahasiswaanPrestasiData[]) {
    let pkm = 0
    let mhsBerprestasi = 0
    let lombaNasional = 0
    let lombaInternasional = 0

    prestasis.forEach((prestasi) => {
      if (prestasi.thnAkademik === this.currentYear && prestasi.getPrestasiMhs) {
        pkm += prestasi.getPrestasiMhs.jmlhPKM
        mhsBerprestasi += prestasi.getPrestasiMhs.jumlhMapres
        lombaNasional += prestasi.getPrestasiMhs.jumlhLombaNasional
        lombaInternasional += prestasi.getPrestasiMhs.jumlhLombaInter
      }
    })

    console.log([
      { name: 'PKM', value: pkm },
      { name: 'Mahasiswa Berprestasi', value: mhsBerprestasi },
      { name: 'Lomba Nasional', value: lombaNasional },
      { name: 'Lomba Internasional', value: lombaInternasional },
    ])

    return [
      { name: 'PKM', value: pkm },
      { name: 'Mahasiswa Berprestasi', value: mhsBerprestasi },
      { name: 'Lomba Nasional', value: lombaNasional },
      { name: 'Lomba Internasional', value: lombaInternasional },
    ]
  }

  transformsTracerData(tracers: StatusLulusan[]) {
    const filteredByLulusanTahun = tracers.filter((tracer) => tracer.lulusanTahun === this.currentYear - 5)

    const maxTahunInput = Math.max(...filteredByLulusanTahun.map((tracer) => tracer.tahun_Input))

    let bekerja = 0
    let lanjutStudi = 0
    let magang = 0
    let wirausaha = 0
    let belumBekerja = 0

    filteredByLulusanTahun.forEach((tracer) => {
      if (tracer.tahun_Input === maxTahunInput) {
        bekerja += tracer.presentase_Bekerja
        lanjutStudi += tracer.presentase_LanjutStudi
        magang += tracer.presentase_Internship
        wirausaha += tracer.presentase_Wirausaha
        belumBekerja += tracer.presentase_BelumKerja
      }
    })

    console.log([
      { name: 'Bekerja', value: bekerja },
      { name: 'Lanjut Studi', value: lanjutStudi },
      { name: 'Magang', value: magang },
      { name: 'Wirausaha', value: wirausaha },
      { name: 'Belum Bekerja', value: belumBekerja },
    ])

    return [
      { name: 'Bekerja', value: bekerja },
      { name: 'Lanjut Studi', value: lanjutStudi },
      { name: 'Magang', value: magang },
      { name: 'Wirausaha', value: wirausaha },
      { name: 'Belum Bekerja', value: belumBekerja },
    ]
  }

  exportToCSVPrestasiMhs(): void {
    const header = [
      'Tahun Akademik',
      'Semester',
      'Jumlah PKM',
      'Jumlah Mahasiswa Berprestasi',
      'Jumlah Lomba Nasional',
      'Jumlah Lomba Internasional',
      'Jumlah Inbound',
      'Jumlah Outbond',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.dataSourcePrestasi.data.map((row) => [
      row.thnAkademik,
      row.semester,
      row.getPrestasiMhs.jmlhPKM,
      row.getPrestasiMhs.jumlhMapres,
      row.getPrestasiMhs.jumlhLombaNasional,
      row.getPrestasiMhs.jumlhLombaInter,
      row.getPrestasiMhs.jumlhInbound,
      row.getPrestasiMhs.jumlhOutbound,
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
    link.setAttribute('download', 'Data Prestasi Mahasiswa Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  exportToCSVTracerMhs(): void {
    const header = [
      'Tahun Lulusan',
      'Tahun Input Data',
      'Presentase Lulusan Bekerja',
      'Presentase Lulusan Lanjut Studi',
      'Presentase Lulusan Internship',
      'Presentase Lulusan Wirausaha',
      'Presentase Lulusan Belum Bekerja',
      'Presentase Masa Tunggu Kerja Lulusan di atas 6 Bulan',
      'Presentase Masa Tunggu Kerja Lulusan di bawah 6 bulan',
      'Presentase Lulusan sebagai Founder',
      'Presentase Lulusan sebagai Co-Founder',
      'Presentase Lulusan sebagai Staff',
      'Presentase Lulusan sebagai Freelancer',
      'Presentase Tempat Kerja Lulusan di BUMN',
      'Presentase Tempat Kerja Lulusan di Instansi Multilateral',
      'Presentase Tempat Kerja Lulusan di Pemerintahan',
      'Presentase Tempat Kerja Lulusan di Organisasi Nonprofit',
      'Presentase Tempat Kerja Lulusan di Wirausaha',
      'Presentase Tempat Kerja Lulusan di Tempat lainnya',
      'Presentase Pekerjaan Lulusan di Tingkat lokal',
      'Presentase Pekerjaan Lulusan di Tingkat nasional',
      'Presentase Pekerjaan Lulusan di Tingkat multinasional',
    ]

    // Ambil data dari dataSourceRingkasanPublikasi
    const rows = this.tracers.map((row) => [
      row.lulusanTahun,
      row.tahun_Input,
      row.presentase_Bekerja,
      row.presentase_LanjutStudi,
      row.presentase_Internship,
      row.presentase_Wirausaha,
      row.presentase_BelumKerja,
      row.masaTungguKerja.presentase_diatas6Bulan,
      row.masaTungguKerja.presentase_dibawah6Bulan,
      row.posisiJabatan.presentase_Founder,
      row.posisiJabatan.presentase_CoFounder,
      row.posisiJabatan.presentase_Staf,
      row.posisiJabatan.presentase_Freelancer,
      row.jenisTempatKerja.presentase_BUMN,
      row.jenisTempatKerja.presentase_Organisasi_Multilateral,
      row.jenisTempatKerja.presentase_Instansi_Pemerintah,
      row.jenisTempatKerja.presentase_Organisasi_NonProfit,
      row.jenisTempatKerja.presentase_Wirausaha,
      row.jenisTempatKerja.presentase_Lainnya,
      row.tingkatTempatKerja.presentase_Lokal,
      row.tingkatTempatKerja.presentase_Nasional,
      row.tingkatTempatKerja.presentase_MultiNasional,
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
    link.setAttribute('download', 'Data Tracer Mahasiswa Departemen ESL.csv')
    document.body.appendChild(link)
    link.click()
    document.body.removeChild(link)
  }

  addSpaceBeforeUppercase(str: string): string {
    return str.replace(/([a-z])([A-Z])/g, '$1 $2')
  }

  // API CALL
  getTracers(): void {
    this.isTracerLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.getTracers().subscribe(
      (data: KemahasiswaanTracerData[]) => {
        this.isTracerLoadingResults = false

        data.forEach((item) => {
          item.statusLulusan.forEach((status) => {
            status.lulusanTahun = item.lulusanTahun
          })
        })

        let allStatusLulusan: StatusLulusan[] = []

        data.forEach((item) => {
          item.statusLulusan.forEach((status) => {
            allStatusLulusan.push(status)
          })
        })

        this.tracers = allStatusLulusan

        this.proyeksiTracer = this.transformsTracerData(allStatusLulusan)

        this.dataSourceTracer.data = allStatusLulusan
        this.dataSourceTracer.paginator = this.paginatorTracer
        this.dataSourceTracer.sort = this.sortTracer
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isTracerLoadingResults = false
        })
      }
    )
  }

  addTracer(tracer: KemahasiswaanTracerRequest): void {
    this.isTracerLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.addTracer(tracer).subscribe(
      (data: MutateKemahasiswaanTracerResponse) => {
        this.isTracerLoadingResults = false
        this.onCloseModalAddTracer()
        this.getTracers()
        this.toastrService.success(`Tracer berhasil ditambah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isTracerLoadingResults = false
        })
      }
    )
  }

  editTracer(id: string, tracer: MutateStatusLulusan): void {
    this.isTracerLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.editTracer(id, tracer).subscribe(
      (data: MutateKemahasiswaanTracerResponse) => {
        this.isTracerLoadingResults = false
        this.onCloseModalEditTracer()
        this.getTracers()
        this.toastrService.success(`Tracer berhasil diubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isTracerLoadingResults = false
        })
      }
    )
  }

  deleteTracer(id: string): void {
    this.isTracerLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.kemahasiswaanService.deleteTracer(id).subscribe(
            (data: MutateKemahasiswaanTracerResponse) => {
              this.isTracerLoadingResults = false
              this.getTracers()
              this.toastrService.success(`Tracer berhasil terhapus`, 'success', { timeOut: 2500 })
            },
            (err: CommonError) => {
              err.messages.forEach((msg) => {
                this.toastrService.error(msg)
                this.isTracerLoadingResults = false
              })
            }
          )
        }
      })
  }

  getPrestasis(): void {
    this.isPrestasiLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.getPrestasis().subscribe(
      (data: KemahasiswaanPrestasiData[]) => {
        this.isPrestasiLoadingResults = false
        this.prestasis = data
        this.proyeksiPrestasi = this.transformsPrestasiData(data)
        this.dataSourcePrestasi.data = data
        this.dataSourcePrestasi.paginator = this.paginatorPrestasi
        this.dataSourcePrestasi.sort = this.sortPrestasi
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isPrestasiLoadingResults = false
        })
      }
    )
  }

  addPrestasi(prestasi: KemahasiswaanPrestasiRequest): void {
    this.isPrestasiLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.addPrestasi(prestasi).subscribe(
      (data: MutateKemahasiswaanPrestasiResponse) => {
        this.isPrestasiLoadingResults = false
        this.onCloseModalAddPrestasi()
        this.getPrestasis()
        this.toastrService.success(`Prestasi berhasil ditambah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isPrestasiLoadingResults = false
        })
      }
    )
  }

  editPrestasi(id: string, prestasi: PrestasiMhs): void {
    this.isPrestasiLoadingResults = true
    this.sub$.sink = this.kemahasiswaanService.editPrestasi(id, prestasi).subscribe(
      (data: MutateKemahasiswaanPrestasiResponse) => {
        this.isPrestasiLoadingResults = false
        this.onCloseModalEditPrestasi()
        this.getPrestasis()
        this.toastrService.success(`Prestasi berhasil diubah`, 'success', { timeOut: 2500 })
      },
      (err: CommonError) => {
        err.messages.forEach((msg) => {
          this.toastrService.error(msg)
          this.isPrestasiLoadingResults = false
        })
      }
    )
  }

  deletePrestasi(id: string): void {
    this.isPrestasiLoadingResults = true
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')}?`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.kemahasiswaanService.deletePrestasi(id).subscribe(
            (data: MutateKemahasiswaanPrestasiResponse) => {
              this.isPrestasiLoadingResults = false
              this.getPrestasis()
              this.toastrService.success(`Prestasi berhasil terhapus`, 'success', { timeOut: 2500 })
            },
            (err: CommonError) => {
              err.messages.forEach((msg) => {
                this.toastrService.error(msg)
                this.isPrestasiLoadingResults = false
              })
            }
          )
        }
      })
  }
}
