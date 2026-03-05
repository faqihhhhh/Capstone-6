import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { AuthGuard } from '@core/security/auth.guard'

import { PendidikanComponent } from './pendidikan.component'

const routes: Routes = [
  {
    path: '',
    component: PendidikanComponent,
    data: { claimType: 'kependidikan_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PendidikanRoutingModule {}
