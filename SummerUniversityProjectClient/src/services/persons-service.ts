import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IPerson} from "../interfaces/IPerson";
import { IPersonInRelationship } from "interfaces/IPersonInRelationship";
import { IPersonData } from "interfaces/IPersonData";
import { IPersonPredecessor } from "interfaces/IPersonPredecessor";
import { INthChildInFamily } from "interfaces/INthChildInFamily";
import { IFamilyTree } from "interfaces/IFamilyTree";

export var log = LogManager.getLogger('PersonsService');

@autoinject
export class PersonsService extends BaseService<IPerson> {
  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Persons');
  }

  getDaughtersAndSons(): Promise<[IPersonData]> {
    let url = this.appConfig.apiUrl + 'Persons/GetDaughtersAndSons';
      
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
  }

  getPersonsRelatedFamilyMembers(id: number): Promise<IFamilyTree> {
    let url = this.appConfig.apiUrl + 'Persons/GetPersonsRelatedFamilyMembers/' + id;
      
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
  }

  getPersonWithMostPredecessors(): Promise<IPersonPredecessor> {
    let url = this.appConfig.apiUrl + 'Persons/GetPersonWithMostPredecessors';
      
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
  }

  getNthChildInFamily(id: number): Promise<INthChildInFamily> {
    let url = this.appConfig.apiUrl + 'Persons/GetNthChildInFamily/' + id;
      
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
  }

  fetchForRelationships(id: number): Promise<IPersonInRelationship> {
    let url = this.appConfig.apiUrl + 'Persons/GetPersonRelationships/' + id;
      
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
  }
}