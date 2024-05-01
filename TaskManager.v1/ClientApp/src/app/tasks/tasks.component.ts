import { Component, Inject, OnInit } from '@angular/core';
import { TaskRequest, Tasks } from '../../../Models/models';
import { HttpClient } from '@angular/common/http';
import { FormControl, FormGroup, RequiredValidator, Validators } from '@angular/forms';
import { TaskService } from '../../Services/task.service';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {

  public tasks: any;
  public tasksEnable: boolean = true;
  public createEnabled: boolean = true;
  public updateEnabled: boolean = true;
  public buttonText: string = '';
  errorText: string = ""
  option: string = 'Title';
  title: string = '';
  number: number = 0;
  priority: string = 'Low';
  status: string = 'To Do'
  DtaeFrom = Date.now().toLocaleString();;
  DateTo = Date.now().toLocaleString();;
  titleEnabled = true
  numberEnabled = false
  priorityEnabled = false
  statusEnabled = false
  dateEnabled = false

  numberSort = true
  createdSort = true
  dueSort = true

  sortFlag = true

  taskForm: any;
  formStatus: string = '';
  formdata: any = {};
  priorityies: string[] = ['Low', 'Moderate', 'High'];
  statuses: string[] = ['To Do', 'In Progress', 'Done']
  options: string[] = ['Title', 'Number', 'Priority','Status', 'created Date']
  sessionID: string = ''

  notificationsCount = 0

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private taskService: TaskService,
    private router: Router
  ) {
    if (localStorage.getItem('sessionID') == null) {
      this.login()
    }
  }

  ngOnInit(): any {
    if (localStorage.getItem('sessionID') == null) {
      this.login()
    }
    else {
      this.search('id')
      this.taskForm = new FormGroup({
        id: new FormControl(0),
        title: new FormControl(null, [Validators.required, Validators.maxLength(40)]),
        description: new FormControl(null, Validators.required),
        status: new FormControl('To Do', Validators.required),
        priority: new FormControl('Low', Validators.required),
        dueDate: new FormControl(null, Validators.required),
        createdDate: new FormControl(null, ),
        modifiedDate: new FormControl(null, ),
      })

      this.createEnabled = false;
      this.updateEnabled = false
    }

  };

  editTask(taskID: any): any {
    let request = {
      id: taskID,
      token: localStorage.getItem('sessionID')
    };

    this.buttonText = 'Update'
    this.updateEnabled = true
    this.createEnabled = true
    this.tasksEnable = false

    
    this.taskService.getTaskById(request).subscribe((data) => {
      if (data.code == '501-1') {
        this.goToLogin()
      }
      else {
        this.taskForm.controls['id'].setValue(data.id)
        this.taskForm.controls['title'].setValue(data.title)
        this.taskForm.controls['description'].setValue(data.description)
        this.taskForm.controls['priority'].setValue(data.priority)
        this.taskForm.controls['status'].setValue(data.status)
        this.taskForm.controls['dueDate'].setValue(data.dueDate)
        this.taskForm.controls['createdDate'].setValue(data.createdDate)
        this.taskForm.controls['modifiedDate'].setValue(data.modifiedDate)
      }
     
    })
  }

  OnSubmitted() {
    console.log(this.taskForm)

    if (!this.taskForm.valid) {
      this.errorText = "fill all the fields that are marked with '*'"
    }
    else {
      this.errorText = '';
      this.updateEnabled = false
      this.createEnabled = false
      this.tasksEnable = true
      let request = {
        id: this.taskForm.value['id'],
        title: this.taskForm.value['title'],
        Description: this.taskForm.value['description'],
        priority: this.taskForm.value['priority'],
        status: this.taskForm.value['status'],
        dueDate: this.taskForm.value['dueDate'],
        createdby: 0,
        token: localStorage.getItem('sessionID')
      }
      if (request.id == 0) {
        this.taskService.createTask(request).subscribe((data) => {
          if (data.code == '501-1') {
            this.goToLogin()
          }
          else {
            this.getAllTasks();
          }
        })
      }
      else {
        this.taskService.updateTask(request).subscribe((data) => {
          if (data.code == '501-1') {
            this.goToLogin()
          }
          else {
            this.getAllTasks();
          }

        })

      }
      this.resetForm()    
    }  
    
  }

  getAllTasks() {
    this.refresh()
    let payload = {
      token: localStorage.getItem('sessionID')
    };
    this.taskService.getAllTasks(payload).subscribe((data) => {
      if (data.code == '501-1') {
        this.goToLogin()
      }
      else {
        this.tasks = data
      }
    });
  }

  deleteTask(taskId: any) {
    let request = {
      id: taskId,
      token: localStorage.getItem('sessionID')
    };
    if (confirm("Deleted task no longer visible in the list")) {
      this.taskService.deleteTask(request).subscribe((data) => {
        if (data.code == '501-1') {
          this.goToLogin()
        }
        else {
          this.getAllTasks();
        }
        
      })
    }
  }

  create() {
    this.buttonText = 'Create'
    this.updateEnabled = false
    this.createEnabled = true
    this.tasksEnable = false  
  }

  cancle() {
    this.errorText = '';
    this.createEnabled = false;
    this.updateEnabled = false
    this.tasksEnable = true
    this.resetForm()
  }

  resetForm() {
    this.taskForm.reset({
      id: 0,
      title: null,
      Description: null,
      priority: 'Low',
      status: 'To Do',
      dueDate: null,
      createdby: 0,
    })
  }

  goToLogin() {
    alert("previous session expires, Please login again")
    this.login();
  }

  login() {
    this.router.navigate(['login']);
  }

  search(field: any) {
    let request = this.payload(field)
    this.taskService.getTaskBycriteria(request).subscribe((data) => {
      if (data.code == '501-1') {
        this.goToLogin()
      }
      else {
        this.tasks = data
      }
    });
  }

  onSearchChange() {
    //['Number', 'Title', 'Priority','Status', 'Date']
    console.log('on option change ' + this.option)
    if (this.option == 'Title') {

      this.titleEnabled = true
      this.priorityEnabled = false
      this.statusEnabled = false
      this.dateEnabled = false
      this.numberEnabled = false

    } else if (this.option == 'Number') {

      this.titleEnabled = false
      this.priorityEnabled = false
      this.statusEnabled = false
      this.dateEnabled = false
      this.numberEnabled = true

    } else if (this.option == 'Priority') {

      this.titleEnabled = false
      this.priorityEnabled = true
      this.statusEnabled = false
      this.dateEnabled = false
      this.numberEnabled = false


    } else if (this.option == 'Status') {

      this.titleEnabled = false
      this.priorityEnabled = false
      this.statusEnabled = true
      this.dateEnabled = false
      this.numberEnabled = false

    } else {

      this.titleEnabled = false
      this.priorityEnabled = false
      this.statusEnabled = false
      this.dateEnabled = true
      this.numberEnabled = false

    }
  }

  payload(field:any): any {
    let request = {
      criteria: '',
      value: '',
      sortField: field,
      sort: '',
      token: localStorage.getItem('sessionID')
    }
    if (this.sortFlag == true) {
      request.sort = 'ASC'
      this.sortFlag = false
    }
    else {
      request.sort = 'DESC'
      this.sortFlag = true
    }
    if (this.option == 'Number') {

      request.criteria = 'Number'
      request.value = this.number.toString()

    } else if (this.option == 'Title') {

      request.criteria = 'Title'
      request.value = this.title

    } else if (this.option == 'Priority') {

      request.criteria = 'Priority'
      request.value = this.priority

    } else if (this.option == 'Status') {

      request.criteria = 'Status'
      request.value = this.status

    } else {

      request.criteria = 'createdDate'
      request.value = this.DtaeFrom.replace("T", " ") + '___' + this.DateTo.replace("T", " ")
    }

    return request
  }

  refresh() {
    this.option = 'Title'
    this.titleEnabled = true
    this.priorityEnabled = false
    this.statusEnabled = false
    this.dateEnabled = false
    this.numberEnabled = false
    this.priority = 'Low';
    this.title = '';
    this.number = 0;
    this.DtaeFrom = Date.now().toLocaleString();
    this.DateTo = Date.now().toLocaleString();
    this.status = 'To Do'
    this.search('id')
  }

  sort(field:any) {
    console.log(field)
    if (field == 'id') {
      if (this.numberSort == true) {
        this.tasks.sort((a: any, b: any) => a.id - b.id)
        this.numberSort = false
      }
      else {
        this.tasks.sort((a: any, b: any) => b.id - a.id)
        this.numberSort = true
      }
    }
    if (field == 'createdDate') {
      if (this.createdSort == true) {
        this.tasks.sort((a: any, b: any) => Date.parse(a.createdDate) - Date.parse(b.ccreatedDate))
        this.createdSort = false
      }
      else {
        this.tasks.sort((a: any, b: any) => Date.parse(b.createdDate) - Date.parse(a.ccreatedDate))
        this.createdSort = true
      }
    }
  }

}
