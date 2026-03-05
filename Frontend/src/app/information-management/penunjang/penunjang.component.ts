import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core'
import { MatDialog } from '@angular/material/dialog'
import { MatPaginator } from '@angular/material/paginator'
import { MatSort } from '@angular/material/sort'
import { MatTableDataSource } from '@angular/material/table'
import { DocumentResource } from '@core/domain-classes/document-resource'
import { BaseComponent } from 'src/app/base.component'
import { ExcelService } from './excel.service'
import * as FileSaver from 'file-saver'

import * as XLSX from 'xlsx'

@Component({
  selector: 'penunjang',
  templateUrl: './penunjang.component.html',
  styleUrls: ['./penunjang.component.scss'],
})
export class PenunjangComponent extends BaseComponent implements AfterViewInit {
  dataSource = new MatTableDataSource<any>()
  displayedColumns: string[] = [
    'nip',
    'nama',
    'jenis_penunjang',
    'nama_kegiatan',
    'deskripsi_kegiatan',
    'tempat_kegiatan',
    'tanggal_kegiatan',
  ]

  constructor(private excelService: ExcelService) {
    super()
  }

  onFileChange(evt: any) {
    const target: DataTransfer = <DataTransfer>evt.target
    if (target.files.length !== 1) throw new Error('Cannot use multiple files')
    const reader: FileReader = new FileReader()
    reader.onload = (e: any) => {
      const bstr: string = e.target.result
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' })
      const wsname: string = wb.SheetNames[0]
      const ws: XLSX.WorkSheet = wb.Sheets[wsname]
      const jsonData = XLSX.utils.sheet_to_json(ws)
      console.log(jsonData)
      this.excelService.setExcelData(jsonData) // Store data globally
    }
    reader.readAsBinaryString(target.files[0])
  }
  @ViewChild(MatSort) sort: MatSort
  @ViewChild(MatPaginator) paginator: MatPaginator

  importExcel() {
    this.dataSource.data = this.excelService.getExcelData()
    console.log(this.dataSource.data)
    this.namaSingle = this.calculateNamaDistribution(this.dataSource.data)
    this.jabFungsiSingle = this.calculateJabatanTerakhirDistribution(this.dataSource.data)
  }

  exportAsExcelFile(): void {
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.dataSource.data)
    const wb: XLSX.WorkBook = XLSX.utils.book_new()
    XLSX.utils.book_append_sheet(wb, ws, 'Penunjang')
    /* save to file */
    XLSX.writeFile(wb, 'Penunjang.xlsx')
  }

  data: any
  dapeg: any
  display = 'none'

  showXAxis = true
  showYAxis = true
  gradient = false
  showLegend = true
  showXAxisLabel = true
  showYAxisLabel = true
  xAxisLabelNama = 'Penunjang'
  yAxisLabelNama = 'Jumlah'
  xAxisLabelJab = 'Jabatan'
  yAxisLabelJab = 'Jumlah'

  namaSingle: any[] = []
  jabFungsiSingle: any[] = []

  ngAfterViewInit() {
    this.dataSource.sort = this.sort
    this.dataSource.paginator = this.paginator
  }

  pageSizeOptions: number[] = [5, 10, 20]
  pageIndex = 0
  pageSize = 10

  onPageChange(event: any) {
    this.pageIndex = event.pageIndex
    this.pageSize = event.pageSize
  }

  openModal() {
    this.display = 'block'
  }
  onCloseHandled() {
    this.display = 'none'
  }

  calculateNamaDistribution(data) {
    const namaCounts = {}
    data.forEach((item) => {
      const nama = item.nama
      namaCounts[nama] = (namaCounts[nama] || 0) + 1
    })

    return Object.keys(namaCounts).map((key) => ({
      name: key,
      value: namaCounts[key],
    }))
  }

  calculateJabatanTerakhirDistribution(data) {
    const jabTerakhirCounts = {}
    data.forEach((item) => {
      const jabTerakhir = item.jab_fungsi_terakhir
      jabTerakhirCounts[jabTerakhir] = (jabTerakhirCounts[jabTerakhir] || 0) + 1
    })

    return Object.keys(jabTerakhirCounts).map((key) => ({
      name: key,
      value: jabTerakhirCounts[key],
    }))
  }
}
