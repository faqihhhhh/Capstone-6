import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { MatTableModule } from '@angular/material/table'
import { MatSortModule } from '@angular/material/sort'
import { MatPaginatorModule } from '@angular/material/paginator'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { MatIconModule } from '@angular/material/icon'
import { PublikasiComponent } from './publikasi.component'
import { PublikasiRoutingModule } from './publikasi-routing.module'
import { NgxChartsModule } from '@swimlane/ngx-charts'

@NgModule({
  declarations: [PublikasiComponent],
  imports: [
    CommonModule,
    PublikasiRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatIconModule,
    NgxChartsModule,
  ],
})
export class PublikasiModule {}
