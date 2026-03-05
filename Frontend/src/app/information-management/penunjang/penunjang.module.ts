import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { MatTableModule } from '@angular/material/table'
import { MatSortModule } from '@angular/material/sort'
import { MatPaginatorModule } from '@angular/material/paginator'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'

import { PenunjangComponent } from './penunjang.component'
import { PenunjangRoutingModule } from './penunjang-routing.module'
import { NgxChartsModule } from '@swimlane/ngx-charts'

@NgModule({
  declarations: [PenunjangComponent],
  imports: [
    CommonModule,
    PenunjangRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    NgxChartsModule,
  ],
})
export class PenunjangModule {}
