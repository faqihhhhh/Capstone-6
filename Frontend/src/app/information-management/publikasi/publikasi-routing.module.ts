import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { AuthGuard } from '@core/security/auth.guard'
import { PublikasiComponent } from './publikasi.component'

const routes: Routes = [
  {
    path: '',
    component: PublikasiComponent,
    data: { claimType: 'publikasi_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PublikasiRoutingModule {}
