% This program calculates the stress to a Linear Viscoelastic Model.
% Viscoelastic Models accepted:
% Maxwell

global stiffness viscosity relaxationTime initialStrain
global stress viscoelasticModel

input = readtable('Input.txt');

viscoelasticModel = cell2mat(table2array(input(1,2)));
stiffness = str2double(cell2mat(table2array(input(2,2))));
viscosity = str2double(cell2mat(table2array(input(3,2))));
initialStrain = str2double(cell2mat(table2array(input(4,2))));
initialTime = str2double(cell2mat(table2array(input(5,2))));
timeStep = str2double(cell2mat(table2array(input(6,2))));
finalTime = str2double(cell2mat(table2array(input(7,2))));

relaxationTime = viscosity/stiffness;

if strcmp(viscoelasticModel, 'Maxwell')
    stress = @(t) Maxwell_Stress(initialStrain, stiffness, relaxationTime, t);
end

numPoints = (finalTime - initialTime) / timeStep;
x = linspace(initialTime, finalTime, numPoints);

y = feval(stress,x);
plot(x,y);