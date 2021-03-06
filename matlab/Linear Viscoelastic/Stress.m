% This program calculates the stress to a Linear Viscoelastic Model.
% Viscoelastic Models accepted:
% Maxwell

global stiffness viscosity relaxationTime initialStrain
global stress viscoelasticModel

viscoelasticModel = 'Maxwell';
stiffness = 3;
viscosity = 32;
initialStrain = 25;
initialTime = 0;
timeStep = 0.1;
finalTime = 300;

relaxationTime = viscosity/stiffness;

if strcmp(viscoelasticModel, 'Maxwell')
    stress = @(t) Maxwell_Stress(initialStrain, stiffness, relaxationTime, t);
end

numPoints = (finalTime - initialTime) / timeStep;
x = linspace(initialTime, finalTime, numPoints);

y = feval(stress,x);
plot(x,y);