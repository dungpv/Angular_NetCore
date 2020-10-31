import { Component, OnInit, OnDestroy } from '@angular/core';
import { MessageConstants } from '@app/shared/constants';
import { PhonggdService, SogdService, NotificationService } from '@app/shared/services';
import { Pagination, Phonggd, Sogd } from '@app/shared/models';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';;
import { PhonggdDetailComponent } from './phonggd-detail/phonggd-detail.component';

@Component({
  selector: 'app-phonggd',
  templateUrl: './phonggd.component.html',
  styleUrls: ['./phonggd.component.css']
})
export class PhonggdComponent extends BaseComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public screenTitle: string;
  // Default
  public bsModalRef: BsModalRef;
  public blockedPanel = false;
  public totalItems = 0;
    /**
   * Paging
   */
  public pageIndex = 1;
  public pageSize = 50;
  public pageDisplay = 20;
  public totalRecords: number;
  public keyword = '';
  public maSoGD = '';
  // Role
  public items: any[];
  public selectedItems = [];
  soGD: Sogd[];
  constructor(private phonggdService: PhonggdService,
    private sogdService: SogdService,
    private notificationService: NotificationService,
    private modalService: BsModalService) {
      super('CONTENT_PHONGGD');
     }

     ngOnInit(): void {
      super.ngOnInit();
      this.loadData();
      this.getSoGD();
  }
  getSoGD(): void {
    this.sogdService.GetSoGD().subscribe(sogd => this.soGD = sogd);
  }
    selectSoGD() {
      this.loadData();
    }
    loadData(selectedMa = null) {
      this.blockedPanel = true;
      this.subscription.add(this.phonggdService.getAllPaging(this.keyword, this.pageIndex, this.pageSize, this.maSoGD)
        .subscribe((response: Pagination<Phonggd>) => {
          this.processLoadData(selectedMa, response);
          setTimeout(() => { this.blockedPanel = false; }, 1000);
        }, error => {
          setTimeout(() => { this.blockedPanel = false; }, 1000);
        }));
    }
    private processLoadData(selectedMa = null, response: Pagination<Phonggd>) {
      this.items = response.items;
      this.pageIndex = this.pageIndex;
      this.pageSize = this.pageSize;
      this.totalRecords = response.totalRecords;
      if (this.selectedItems.length === 0 && this.items.length > 0) {
        this.selectedItems.push(this.items[0]);
      }
      if (selectedMa != null && this.items.length > 0) {
        this.selectedItems = this.items.filter(x => x.Ma === selectedMa);
      }
    }
    pageChanged(event: any): void {
      this.pageIndex = event.page + 1;
      this.pageSize = event.rows;
      this.loadData();
    }
  
    showAddModal() {
      this.bsModalRef = this.modalService.show(PhonggdDetailComponent,
        {
          class: 'modal-lg',
          backdrop: 'static'
        });
      this.bsModalRef.content.savedEvent.subscribe((response) => {
        this.bsModalRef.hide();
        this.loadData();
        this.selectedItems = [];
      });
    }
    showEditModal() {
      if (this.selectedItems.length === 0) {
        this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
        return;
      }
      const initialState = {
        entityId: this.selectedItems[0].id
      };
      this.bsModalRef = this.modalService.show(PhonggdDetailComponent,
        {
          initialState: initialState,
          class: 'modal-lg',
          backdrop: 'static'
        });
        this.subscription.add( this.bsModalRef.content.savedEvent.subscribe((response) => {
          this.bsModalRef.hide();
          this.loadData(response.ma);
        }));
        }
    ngOnDestroy(): void {
      this.subscription.unsubscribe();
    }

}
