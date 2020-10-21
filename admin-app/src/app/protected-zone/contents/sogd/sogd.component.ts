import { Component, OnInit, OnDestroy } from '@angular/core';
import { MessageConstants } from '@app/shared/constants';
import { SogdService, NotificationService } from '@app/shared/services';
import { Pagination, Sogd } from '@app/shared/models';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';;

@Component({
  selector: 'app-sogd',
  templateUrl: './sogd.component.html',
  styleUrls: ['./sogd.component.css']
})
export class SogdComponent extends BaseComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public screenTitle: string;
  // Default
  public bsModalRef: BsModalRef;
  public blockedPanel = false;
  public totalItems = 0;
  // Role
  public items: any[];
  public selectedItems = [];
  constructor(private sogdService: SogdService,
    private notificationService: NotificationService,
    private modalService: BsModalService) {
      super('CONTENT_SOGD');
     }

  ngOnInit(): void {
    super.ngOnInit();
    this.loadData();
  }

  loadData() {
    this.blockedPanel = true;
    this.sogdService.GetSoGD()
      .subscribe((response: any) => {
        this.totalItems = 0;
        this.items = response;
        response.forEach(element => {
          this.totalItems += element.NumberOfUsers;
        });
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      });
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
