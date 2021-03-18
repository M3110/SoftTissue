% Reduced Relaxation Function to Linear Viscoelastic Maxwell Model
function y = Maxwell_ReducedRelaxationFunction(stiffness, relaxationTime, time)

y = stiffness * exp(-time / relaxationTime);

end