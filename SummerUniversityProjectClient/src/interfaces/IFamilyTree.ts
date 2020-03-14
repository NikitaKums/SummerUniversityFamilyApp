import { IPersonData } from "./IPersonData";

export interface IFamilyTree extends IPersonData {
    family: [IFamilyTree]
}