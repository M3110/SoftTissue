% Creep Compliance to Linear Viscoelastic Maxwell Model
function y = Maxwell_CreepCompliance(stiffness, viscosity, time)

y = 1 / stiffness + time / viscosity;

end