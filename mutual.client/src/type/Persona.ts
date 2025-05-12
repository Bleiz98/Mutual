export interface PersonaRegistro {
  nombreRazonSocial: string;
  direccion: string;
  telefono: string;
  email: string;
  dniCuit: number; // si lo trat�s como string por validaci�n
  rol: "Admin" | "Socio" | "Proovedor";
}
