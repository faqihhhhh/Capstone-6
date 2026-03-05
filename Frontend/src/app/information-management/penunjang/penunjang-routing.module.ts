import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { AuthGuard } from '@core/security/auth.guard'

import { PenunjangComponent } from './penunjang.component'

const routes: Routes = [
  {
    path: '',
    component: PenunjangComponent,
    data: { claimType: 'penunjang_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PenunjangRoutingModule {}
