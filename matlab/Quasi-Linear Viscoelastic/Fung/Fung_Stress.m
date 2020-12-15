% This program calculates the stress to a Quasi-Linear Viscoelastic Model.
% Viscoelastic Models accepted:
% Fung

global initialStress mi0 mi1 mi2 mi3 tau1 tau2 tau3
global initialTime timeStep finalTime considerRampTime
global stress softTissueType
global fileName

% Here the values are divided by initial stress to obtain the correct value
% for mi to be used in Redecued Relaxation Function.
% The values hard-coded are obtained to experiments

softTissueType = 'Posterior Cruciate Ligament - Second Relaxation';
initialStress = 1.54;
mi0 = 1.38406 / initialStress;
mi1 = 0.03412 / initialStress;
mi2 = 0.04735 / initialStress;
mi3 = 0.07758 / initialStress;
tau1 = 1.87972;
tau2 = 14.66308;
tau3 = 139.22114;

initialTime = 0;
timeStep = 0.1;
finalTime = 300;

considerRampTime = false;

time = initialTime;

fileName = strcat('Stress_',softTissueType,'.txt');
fileID = fopen(fileName,'w');
fprintf(fileID,'%6s %6s\n','Time','Stress');

while time <= finalTime
    
    if (considerRampTime == false)
        stress = initialStress * Fung_SimplifiedReducedRelaxationFunction(time,mi0,mi1,mi2,mi3,tau1,tau2,tau3);
    else
    end
    
    fprintf(fileID,'%6.2f %12.8f\n', time, stress);
    
    time = time + timeStep;
    
end

fclose(fileID);