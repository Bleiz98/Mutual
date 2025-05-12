import { useState } from 'react';
import axios from 'axios';

export default function Registro() {
    const [formData, setFormData] = useState({
        dniCuit: '',
        username: '',
        passwordHash: '',
        rol: ''
    });

    const [mensaje, setMensaje] = useState('');
    const [error, setError] = useState('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');
        try {
            await axios.post('/api/persona/registro', formData);
            setMensaje('✅ Registro exitoso');
        } catch (error) {
            console.error(error);
            setMensaje('Error al registrar');
        }
    };

    return (
        <div className="container mt-5">
            <div className="card shadow-sm">
                <div className="card-body">
                    <h2 className="card-title mb-4 text-center">Registro de Usuario</h2>

                    <form onSubmit={handleSubmit}>
                        <div className="mb-3">
                            <label className="form-label">DNI / CUIT</label>
                            <input
                                name="dniCuit"
                                className="form-control"
                                value={formData.dniCuit}
                                onChange={handleChange}
                                required
                            />
                        </div>

                        <div className="mb-3">
                            <label className="form-label">Rol</label>
                            <input
                                name="rol"
                                className="form-control"
                                value={formData.rol}
                                onChange={handleChange}
                                required
                            />
                        </div>

                        <div className="d-grid">
                            <button type="submit" className="btn btn-primary">Registrarse</button>
                        </div>
                    </form>

                    {mensaje && <div className="alert alert-success mt-3">{mensaje}</div>}
                    {error && <div className="alert alert-danger mt-3">{error}</div>}
                </div>
            </div>
        </div>
    );
}
