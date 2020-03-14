import {LogManager, View, autoinject} from "aurelia-framework";
import {RouteConfig, NavigationInstruction} from "aurelia-router";
import { IPersonData } from "interfaces/IPersonData";
import { PersonInRelationshipService } from "services/person-in-relationship-service";
import { PersonsService } from "services/persons-service";
import { IPersonPredecessor } from "interfaces/IPersonPredecessor";
import { INthChildInFamily } from "interfaces/INthChildInFamily";

export var log = LogManager.getLogger('Home');

@autoinject
export class Home {

  private youngestUncle: IPersonData;
  private youngestAunt: IPersonData;
  private showPredecessorsBoolean: boolean = false;
  private werePredecessorsLoaded: boolean = false;
  private whichChildInFamily: INthChildInFamily;
  private predecessorData: IPersonPredecessor;
  private childrenDropdown: [IPersonData]
  private whichChildId: number;

  constructor(private personsService: PersonsService,
              private personInRelationshipsService: PersonInRelationshipService) {
    log.debug('constructor');
  }

  // ============ View LifeCycle events ==============

  loadYoungestAunt() {
    this.personInRelationshipsService.fetchYoungestAunt().then(
      jsonData => {
        log.debug("loadYoungestAunt", + jsonData)
        this.youngestAunt = jsonData;
      }
    );
  }

  showPredecessors() {
    this.showPredecessorsBoolean = !this.showPredecessorsBoolean;

    if (!this.werePredecessorsLoaded) {
      this.getPersonWithMostPredecessors();
    }
  }

  getNthChildInFamily() {
    log.debug("getNthChildInFamily" + this.whichChildId);

    if (String(this.whichChildId) === "null"){
      return;
    }

    this.personsService.getNthChildInFamily(this.whichChildId).then(
      jsonData => {
        log.debug("getNthChildInFamily", + jsonData)
        this.whichChildInFamily = jsonData;
      }
    );
  }

  getPersonWithMostPredecessors() {
    log.debug("getPersonWithMostPredecessors");
    this.personsService.getPersonWithMostPredecessors().then(
      jsonData => {
        log.debug("getPersonWithMostPredecessors", + jsonData)
        this.predecessorData = jsonData;
        this.werePredecessorsLoaded = true;
      }
    );
  }

  loadYoungestUncle() {
    this.personInRelationshipsService.fetchYoungestUncle().then(
      jsonData => {
        log.debug("loadYoungestUncle", + jsonData)
        this.youngestUncle = jsonData;
      }
    );
  }

  loadChildren() {
    this.personsService.getDaughtersAndSons().then(
      jsonData => {
        log.debug("loadChildren", + jsonData)
        this.childrenDropdown = jsonData;
      }
    );
  }

  created(owningView: View, myView: View) {
    log.debug('created');
  }

  bind(bindingContext: Object, overrideContext: Object) {
    log.debug('bind');
  }

  attached() {
    log.debug('attached');
    this.loadYoungestAunt();
    this.loadYoungestUncle();
    this.loadChildren();
  }

  detached() {
    log.debug('detached');
  }

  unbind() {
    log.debug('unbind');
  }

  // ============= Router Events =============
  canActivate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('canActivate');
  }

  activate(params: any, routerConfig: RouteConfig, navigationInstruction: NavigationInstruction) {
    log.debug('activate');
  }

  canDeactivate() {
    log.debug('canDeactivate');
  }

  deactivate() {
    log.debug('deactivate');
  }
}