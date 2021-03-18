% Strain to Linear Viscoelastic Maxwell Model
function y = Maxwell_Strain(initialStress, stiffness, viscosity, time)

creepCompliance = Maxwell_CreepCompliance(stiffness, viscosity, time);

y = (initialStress / stiffness) * creepCompliance;

end