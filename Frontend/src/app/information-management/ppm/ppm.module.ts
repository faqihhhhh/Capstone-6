import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { MatTableModule } from '@angular/material/table'
import { MatSortModule } from '@angular/material/sort'
import { MatPaginatorModule } from '@angular/material/paginator'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { MatIconModule } from '@angular/material/icon'
import { PpmComponent } from './ppm.component'
import { PpmRoutingModule } from './ppm-routing.module'
import { NgxChartsModule } from '@swimlane/ngx-charts'

@NgModule({
  declarations: [PpmComponent],
  imports: [
    CommonModule,
    PpmRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    NgxChartsModule,
    MatIconModule,
  ],
})
export class PpmModule {}
