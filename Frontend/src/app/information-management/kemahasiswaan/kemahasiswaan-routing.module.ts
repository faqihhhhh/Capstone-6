import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { AuthGuard } from '@core/security/auth.guard'

import { KemahasiswaanComponent } from './kemahasiswaan.component'

const routes: Routes = [
  {
    path: '',
    component: KemahasiswaanComponent,
    data: { claimType: 'kemahasiswaan_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class KemahasiswaanRoutingModule {}
