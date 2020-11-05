import { Component, OnInit, OnDestroy } from '@angular/core';
import { MessageConstants } from '@app/shared/constants';
import { TruongService, PhonggdService, SogdService, DanhMucService, NotificationService } from '@app/shared/services';
import { Pagination, Phonggd, Sogd, Truong, BaseDanhMuc } from '@app/shared/models';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';;
import { TruongDetailComponent } from './truong-detail/truong-detail.component';

@Component({
  selector: 'app-truong',
  templateUrl: './truong.component.html',
  styleUrls: ['./truong.component.css']
})
export class TruongComponent extends BaseComponent implements OnInit, OnDestroy {

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
  public maCapHoc = '';
  // Role
  public items: any[];
  public selectedItems = [];
  soGD: Sogd[];
  capHoc: BaseDanhMuc[];
  constructor(private phonggdService: PhonggdService,
    private sogdService: SogdService,
    private truongService: TruongService,
    private danhMucService: DanhMucService,
    private notificationService: NotificationService,
    private modalService: BsModalService) {
    super('CONTENT_TRUONG');
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.loadData();
    this.getSoGD();
    this.getCapHoc();
  }
  getSoGD(): void {
    this.sogdService.GetSoGD().subscribe(sogd => this.soGD = sogd);
  }
  getCapHoc(): void {
    this.danhMucService.getDMCapHoc().subscribe(caphoc => this.capHoc = caphoc);
  }  
  selectSoGD() {
    this.loadData();
  }
  selectCapHoc() {
    this.loadData();
  } 
  loadData(selectedMa = null) {
    this.blockedPanel = true;
    this.subscription.add(this.truongService.getAllPaging(this.keyword, this.pageIndex, this.pageSize, this.maSoGD, this.maCapHoc)
      .subscribe((response: Pagination<Truong>) => {
        this.processLoadData(selectedMa, response);
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }
  private processLoadData(selectedMa = null, response: Pagination<Truong>) {
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
    this.bsModalRef = this.modalService.show(TruongDetailComponent,
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
    this.bsModalRef = this.modalService.show(TruongDetailComponent,
      {
        initialState: initialState,
        class: 'modal-lg',
        backdrop: 'static'
      });
    this.subscription.add(this.bsModalRef.content.savedEvent.subscribe((response) => {
      this.bsModalRef.hide();
      this.loadData(response.ma);
    }));
  }

  deleteItems() {
    const id = this.selectedItems[0].id;
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemsConfirm(id));
  }
  deleteItemsConfirm(id) {
    this.blockedPanel = true;
    this.subscription.add(this.truongService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
      this.selectedItems = [];
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }));
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
