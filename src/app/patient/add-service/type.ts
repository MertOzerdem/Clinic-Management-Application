export interface IType 
{
    type : string;
}

export class Type implements IType{
    
    constructor(public type: string){}
    
}
