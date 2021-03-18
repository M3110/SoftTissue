% Stress to Linear Viscoelastic Maxwell Model
function y = Maxwell_Stress(initialStrain, stiffness, relaxationTime, time)

reducedRelaxationFunction = Maxwell_ReducedRelaxationFunction(stiffness, relaxationTime, time);

y = initialStrain * reducedRelaxationFunction;

end