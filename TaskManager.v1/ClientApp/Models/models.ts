import { Data } from "popper.js";

export class WeatherForecast {
  date ?: string;
  temperatureC ?: number;
  temperatureF ?: number;
  summary ?: string;
}

export class Tasks {
  id?: number;
  title?: string;
  description?: string;
  priority?: string;
  status?: string;
  createdDate?: Date;
  modifiedDate?: Date;
  dueDate?: Data;
}
export class TaskRequest {
  id?: number = 0;
  title?: string;
  Description?: string;
  priority?: string;
  status?: string;
  dueDate?: Data;
  createdby?: number = 0;
  token?: string;
}

//model object for task
//  {
//    "id": 6,
//    "title": "title_update",
//    "description": "des_updaet",
//    "status": "status_update",
//    "priority": "prio_update",
//    "createdDate": "2024-04-20T10:11:58",
//    "modifiedDate": "2024-04-20T15:48:28.637",
//    "createdBy": 2,
//    "dueDate": "2024-04-20T10:17:01"
//  }
