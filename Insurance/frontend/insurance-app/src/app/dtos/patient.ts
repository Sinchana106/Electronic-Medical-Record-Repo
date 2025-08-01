export interface Patient {
  id?: string;
  name: string;
  contactNo: string;
  type: string;
  age: number;
  insuredBy: string;
  visitType: string[];
  description?: string; // Optional if empty string is allowed
}
