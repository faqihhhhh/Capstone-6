import { Injectable } from '@angular/core'

@Injectable({
  providedIn: 'root',
})
export class ExcelService {
  private excelData: any

  setExcelData(data: any) {
    this.excelData = data
  }

  getExcelData() {
    return this.excelData
  }
}
