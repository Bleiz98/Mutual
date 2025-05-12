export interface PersonaRegistro {
  nombreRazonSocial: string;
  direccion: string;
  telefono: string;
  email: string;
  dniCuit: number; // si lo tratás como string por validación
  rol: "Admin" | "Socio" | "Proovedor";
}
