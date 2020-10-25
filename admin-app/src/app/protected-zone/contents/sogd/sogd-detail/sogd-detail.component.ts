import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DanhMucService, SogdService, NotificationService, UtilitiesService } from '@app/shared/services';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MessageConstants } from '@app/shared/constants';
import { SelectItem } from 'primeng/api/selectitem';
import { Sogd, DMTinh, DMCumThiDua } from '@app/shared/models';

@Component({
  selector: 'app-sogd-detail',
  templateUrl: './sogd-detail.component.html',
  styleUrls: ['./sogd-detail.component.css']
})
export class SogdDetailComponent implements OnInit, OnDestroy   {

  constructor(
    public bsModalRef: BsModalRef,
    private danhMucService: DanhMucService,
    private sogdService: SogdService,
    private notificationService: NotificationService,
    private utilitiesService: UtilitiesService,
    private fb: FormBuilder) {
  }

  private subscription = new Subscription();
  public entityForm: FormGroup;
  public dialogTitle: string;
  private savedEvent: EventEmitter<any> = new EventEmitter();
  public dmTinh: any[] = [];
  public dmCumThiDua: any[] = [];
  public entityMa: string;
  public btnDisabled = false;

  public blockedPanel = false;

  public sogd: SelectItem[] = [];

  // Validate
  validation_messages = {
    'ma': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 20 kí tự' }
    ],
    'ten': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 50 kí tự' }
    ],
    'maTinh': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ], 
    'maCumThiDua': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'thuTu': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ]
  };

  ngOnInit() {
    this.entityForm = this.fb.group({
      'ma': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(20)
      ])),
      'ten': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(50)
      ])),
      'diaChi': new FormControl(''),
      'dienThoai': new FormControl(''),
      'email': new FormControl(''),
      'website': new FormControl(''),
      'thuTu': new FormControl(null, Validators.required),
      'maTinh': new FormControl(null, Validators.required),
      'maCumThiDua': new FormControl(null, Validators.required)
    });
    this.subscription.add(
      this.danhMucService.getDMTinh()
      .subscribe((response: DMTinh[]) => {
        response.forEach(element => {
          this.dmTinh.push({
            value: element.ma,
            label: element.ten
          });
        });
        if (this.entityMa) {
          this.dialogTitle = 'Cập nhật';
          this.loadFormDetails(this.entityMa);
        } else {
          this.dialogTitle = 'Thêm mới';
        }
      }));
      this.subscription.add(
        this.danhMucService.getDMCumThiDua()
        .subscribe((response: DMCumThiDua[]) => {
          response.forEach(element => {
            this.dmCumThiDua.push({
              value: element.ma,
              label: element.ten
            });
          });
        }));
  }

  private loadFormDetails(ma: any) {
    this.blockedPanel = true;
    this.subscription.add(this.sogdService.getDetail(ma).subscribe((response: any) => {
      this.entityForm.setValue({
        ma: response.ma,
        ten: response.ten,
        diaChi: response.diaChi,
        dienThoai: response.dienThoai,
        email: response.email,
        website: response.website,
        thuTu: response.thuTu,
        maTinh: response.maTinh,
        maCumThiDua: response.maCumThiDua
      });
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }));
  }


  public saveChange() {
    this.btnDisabled = true;
    this.blockedPanel = true;
    if (this.entityMa) {
      this.subscription.add(this.sogdService.update(this.entityMa, this.entityForm.getRawValue())
        .subscribe(() => {
          this.savedEvent.emit(this.entityForm.value);
          this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
          this.btnDisabled = false;
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }, error => {
          this.notificationService.showError(MessageConstants.UPDATED_NOT_OK_MSG);
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }));
    } else {
      this.subscription.add(this.sogdService.add(this.entityForm.getRawValue())
        .subscribe(() => {
          this.savedEvent.emit(this.entityForm.value);
          this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
          this.btnDisabled = false;
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }, error => {
          this.notificationService.showError(MessageConstants.CREATED_NOT_OK_MSG);
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }));
    }
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
